using Application.Infrastructure.Images;
using Application.WaterBoiler.Models;
using AutoMapper;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Http;
using Repository.WaterBoiler;

namespace Application.WaterBoiler;

internal sealed class WaterBoilerService : IWaterBoilerService
{
    private readonly IMapper mapper;
    private readonly IWaterBoilerRepository repository;
    private readonly IImageService _imageService;

    public WaterBoilerService(IWaterBoilerRepository repository, IMapper mapper, IImageService _imageService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this._imageService = _imageService;
    }

    public async Task<IReadOnlyCollection<GetWaterBoilerModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetWaterBoilerModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetWaterBoilerModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new Exception();
        return mapper.Map<GetWaterBoilerModel>(result);
    }

    public async Task<Guid> AddAsync(AddWaterBoilerModel model, IFormFile imageFile)
    {
        var waterBoiler = mapper.Map<WaterBoilerRecord>(model);
            
        waterBoiler.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "waterBoilers");

        var result = await repository.AddAsync(waterBoiler);

        await repository.SaveChangesAsync();
        
        return result;
    }

    public async Task<UpdateWaterBoilerModel> UpdateAsync(UpdateWaterBoilerModel model, IFormFile? imageFile)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new Exception("WaterBoiler entity not found");
        
        mapper.Map(model, entity);
        
        if (imageFile != null)
        {
            entity.ImagePath = await _imageService.UpdateImageAsync(imageFile, "waterBoilers", entity.ImagePath);
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