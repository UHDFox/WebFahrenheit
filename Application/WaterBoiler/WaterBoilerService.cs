using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Repository.WaterBoiler;

namespace Application.WaterBoiler;

internal sealed class WaterBoilerService : ProductService<WaterBoilerModel, WaterBoilerRecord>, IWaterBoilerService
{
    private readonly IMapper _mapper;
    private readonly IWaterBoilerRepository _repository;
    private readonly IImageService _imageService;

    public WaterBoilerService(IWaterBoilerRepository repository, IMapper mapper, IImageService imageService)
        : base(repository, mapper, imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }
}