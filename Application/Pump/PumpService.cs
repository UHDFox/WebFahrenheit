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

    public async Task<IReadOnlyCollection<GetPumpModel>> GetAllAsync(int offset, int limit)
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

        // Map properties from model to the entity
        mapper.Map(model, entity);

        // If a new image file is provided, save it and update the ImagePath
        if (imageFile != null)
        {
            entity.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "pumps");
        }

        // Update the entity in the repository
        repository.Update(entity);

        // Save changes to the database
        await repository.SaveChangesAsync();

        return model;

    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id); //to check if such an entity exists
        return await repository.DeleteAsync(id);
    }
}