using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Repository.Pump;

namespace Application.Product.Pump;

internal sealed class PumpService : ProductService<PumpModel, PumpRecord>, IPumpService
{
    private readonly IMapper mapper;
    private readonly IPumpRepository repository;
    private readonly IImageService _imageService;

    public PumpService(IPumpRepository repository, IMapper mapper, IImageService imageService) : base(repository,
        mapper, imageService)
    {
        this.repository = repository;
        this.mapper = mapper;
        _imageService = imageService;
    }
}