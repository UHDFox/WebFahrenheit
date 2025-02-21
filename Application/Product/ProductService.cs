using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Images;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository;

namespace Application.Product;

public abstract class ProductService<TModel, TRecord> : IProductService<TModel>
    where TModel : TProduct
    where TRecord : Domain.Domain.Entities.Products.Product
{
    private readonly IRepository<TRecord> _repository;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;
    private readonly ILogger<ProductService<TModel, TRecord>> _logger;

    protected ProductService(IRepository<TRecord> repository, IMapper mapper, IImageService imageService, ILogger<ProductService<TModel, TRecord>> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit)
    {
        return _mapper.Map<IReadOnlyCollection<TModel>>(await _repository.GetAllAsync(offset, limit));
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record == null)
        {
            _logger.LogError($"Record with id: {id} not found in {typeof(TModel).Name}");
            throw new NotFoundException($"Product not found in {typeof(TModel).Name}");
        }
        
        _logger.LogInformation($"Retrieved record with id: {id} in {typeof(TModel).Name}");
        
        return _mapper.Map<TModel>(record);
    }

    public async Task<Guid> AddAsync(TModel model, IFormFile? imageFile)
    {
        var entity = _mapper.Map<TRecord>(model);

        entity.ImagePath =
            await _imageService.SaveImageLocallyAsync(imageFile!, typeof(TModel).Name.Replace("Model", ""));

        var result = await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();
        
        _logger.LogInformation($"Created record with id: {result} in {typeof(TModel).Name}");

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {


            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                _logger.LogError($"Record with id: {id} not found in {typeof(TModel).Name}");
                throw new NotFoundException("Entity not found");
            }

            // Delete the image associated with this entity, if it exists
            if (!string.IsNullOrEmpty(entity.ImagePath))
            {
                await _imageService.DeleteImageAsync(entity.ImagePath);
                _logger.LogInformation($"Deleted image for the record with id: {id}");
            }

            // Delete the entity from the repository
            var result = await _repository.DeleteAsync(id);

            await _repository.SaveChangesAsync();

            _logger.LogInformation(result
                ? $"Deleted record with Id: {id} in {typeof(TModel).Name}"
                : $"Could not delete record with id: {id} in {typeof(TModel).Name}");

            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError($"An error occurred while deleting record with Id: {id}, Message: {ex.Message}");
            return false;
        }
    }

    public async Task<TModel> UpdateAsync(TModel model, IFormFile? imageFile)
    {
        var entity = await _repository.GetByIdAsync(model.Id);
        if (entity == null)
        {
            _logger.LogError($"Record with id: {model.Id} not found in {typeof(TModel).Name}");
            throw new NotFoundException($"Record with id: {model.Id} not found in {typeof(TModel).Name}");
        }

        var oldImagePath = entity.ImagePath;

        _mapper.Map(model, entity);

        if (imageFile != null)
        {
            entity.ImagePath =
                await _imageService.UpdateImageAsync(imageFile, typeof(TModel).Name.Replace("Model", ""), oldImagePath);
            _logger.LogInformation($"Updated image for record with id: {model.Id}");
        }

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        _logger.LogInformation($"Successfully updated record with id: {model.Id} in {typeof(TModel).Name}");
        
        return model;
    }
}