using Application.Infrastructure.Exceptions;
using Application.Infrastructure.Images;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Repository;

namespace Application.Product;

public abstract class ProductService<TModel, TRecord> : IProductService<TModel>
    where TModel : TProduct
    where TRecord : Domain.Domain.Entities.Products.Product
{
    private IRepository<TRecord> _repository;
    private readonly IMapper _mapper;
    private readonly IImageService _imageService;

    public ProductService(IRepository<TRecord> repository, IMapper mapper, IImageService imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }

    public async Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit)
    {
        return _mapper.Map<IReadOnlyCollection<TModel>>(await _repository.GetAllAsync(offset, limit));
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var result = await _repository.GetByIdAsync(id) ?? throw new NotFoundException("Product not found");
        return _mapper.Map<TModel>(result);
    }

    public async Task<Guid> AddAsync(TModel model, IFormFile? imageFile)
    {
        var entity = _mapper.Map<TRecord>(model);

        entity.ImagePath =
            await _imageService.SaveImageLocallyAsync(imageFile!, typeof(TModel).Name.Replace("Model", ""));

        var result = await _repository.AddAsync(entity);

        await _repository.SaveChangesAsync();

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null)
            throw new NotFoundException("Entity not found");

        // Delete the image associated with this entity, if it exists
        if (!string.IsNullOrEmpty(entity.ImagePath))
        {
            await _imageService.DeleteImageAsync(entity.ImagePath);
        }

        // Delete the entity from the repository
        var result = await _repository.DeleteAsync(id);
        await _repository.SaveChangesAsync();

        return result;
    }

    public async Task<TModel> UpdateAsync(TModel model, IFormFile? imageFile)
    {
        var entity = await _repository.GetByIdAsync(model.Id)
                     ?? throw new NotFoundException("Entity not found");

        var oldImagePath = entity.ImagePath;

        _mapper.Map(model, entity);

        if (imageFile != null)
        {
            entity.ImagePath =
                await _imageService.UpdateImageAsync(imageFile, typeof(TModel).Name.Replace("Model", ""), oldImagePath);
        }

        _repository.Update(entity);

        await _repository.SaveChangesAsync();

        return model;
    }

    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}