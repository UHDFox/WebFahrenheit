using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Microsoft.Extensions.Logging;
using Repository.Pump;

namespace Application.Product.Pump;

internal sealed class PumpService : ProductService<PumpModel, PumpRecord>, IPumpService
{
    private readonly IMapper mapper;
    private readonly IPumpRepository repository;
    private readonly IImageService _imageService;

    public PumpService(IPumpRepository repository, IMapper mapper, IImageService imageService, ILogger<PumpService> logger) 
        : base(repository, mapper, imageService, logger)
    {
        this.repository = repository;
        this.mapper = mapper;
        _imageService = imageService;
    }
}