using Application.Infrastructure.Images;
using Application.Radiator.Models;
using AutoMapper;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Http;
using Repository.Radiator;


namespace Application.Radiator;

internal sealed class RadiatorService : IRadiatorService
{
    private readonly IMapper mapper;
    private readonly IRadiatorRepository repository;
    private readonly IImageService _imageService;

    public RadiatorService(IRadiatorRepository repository, IMapper mapper, IImageService _imageService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this._imageService = _imageService;
    }

    public async Task<IReadOnlyCollection<GetRadiatorModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetRadiatorModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetRadiatorModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new Exception();
        return mapper.Map<GetRadiatorModel>(result);
    }

    public async Task<Guid> AddAsync(AddRadiatorModel model, IFormFile imageFile)
    {
        var radiator = mapper.Map<RadiatorRecord>(model);
            
        radiator.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "radiators");

        var result = await repository.AddAsync(radiator);

        await repository.SaveChangesAsync();
        
        return result;
    }

    public async Task<UpdateRadiatorModel> UpdateAsync(UpdateRadiatorModel model, IFormFile? imageFile)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new Exception("Radiator entity not found");
        
        mapper.Map(model, entity);
        
        if (imageFile != null)
        {
            entity.ImagePath = await _imageService.UpdateImageAsync(imageFile, "radiators", entity.ImagePath);
        }

        repository.Update(entity);
        
        await repository.SaveChangesAsync();

        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Entity not found");

        // Delete the image associated with this entity, if it exists
        if (!string.IsNullOrEmpty(entity.ImagePath))
        {
            await _imageService.DeleteImageAsync(entity.ImagePath);
        }

        // Delete the entity from the repository
        var result = await repository.DeleteAsync(id);
        await repository.SaveChangesAsync();

        return result;
    }
}
