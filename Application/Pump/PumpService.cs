using Application.Infrastructure.Images;
using Application.Pump.Models;
using AutoMapper;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Http;
using Repository.Pump;

namespace Application.Pump;

internal sealed class PumpService : IPumpService
{
    private readonly IMapper mapper;
    private readonly IPumpRepository repository;
    private readonly IImageService _imageService;

    public PumpService(IPumpRepository repository, IMapper mapper, IImageService _imageService)
    {
        this.repository = repository;
        this.mapper = mapper;
        this._imageService = _imageService;
    }

    public async Task<IReadOnlyCollection<GetPumpModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await repository.GetTotalAmountAsync();

        return mapper.Map<IReadOnlyCollection<GetPumpModel>>(await repository.GetAllAsync(offset, limit));
    }

    public async Task<GetPumpModel> GetByIdAsync(Guid id)
    {
        var result = await repository.GetByIdAsync(id) ?? throw new Exception();
        return mapper.Map<GetPumpModel>(result);
    }

    public async Task<Guid> AddAsync(AddPumpModel model, IFormFile imageFile)
    {
        var pump = mapper.Map<PumpRecord>(model);
            
        pump.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "pumps");

        var result = await repository.AddAsync(pump);

        await repository.SaveChangesAsync();
        
        return result;
    }

    public async Task<UpdatePumpModel> UpdateAsync(UpdatePumpModel model, IFormFile? imageFile)
    {
        var entity = await repository.GetByIdAsync(model.Id)
                     ?? throw new Exception("Pump entity not found");
        
        mapper.Map(model, entity);
        
        if (imageFile != null)
        {
            entity.ImagePath = await _imageService.UpdateImageAsync(imageFile, "pumps", entity.ImagePath);
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