using Application.Infrastructure.Images;
using Application.Fireplace.Models;
using AutoMapper;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Http;
using Repository.Fireplace;

namespace Application.Fireplace;

internal sealed class FireplaceService : IFireplaceService
{
    private readonly IMapper mapper;
    private readonly IFireplaceRepository repository;
    private readonly IImageService _imageService;

    public FireplaceService(IFireplaceRepository repository, IMapper mapper, IImageService _imageService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this._imageService = _imageService;
    }

    public async Task<IReadOnlyCollection<GetFireplaceModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetFireplaceModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetFireplaceModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new Exception();
        return mapper.Map<GetFireplaceModel>(result);
    }

    public async Task<Guid> AddAsync(AddFireplaceModel model, IFormFile imageFile)
    {
        var fireplace = mapper.Map<FireplaceRecord>(model);
            
        fireplace.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "fireplaces");

        var result = await repository.AddAsync(fireplace);

        await repository.SaveChangesAsync();
        
        return result;
    }

    public async Task<UpdateFireplaceModel> UpdateAsync(UpdateFireplaceModel model, IFormFile? imageFile)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new Exception("Fireplace entity not found");
        
        mapper.Map(model, entity);
        
        if (imageFile != null)
        {
            entity.ImagePath = await _imageService.UpdateImageAsync(imageFile, "fireplaces", entity.ImagePath);
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