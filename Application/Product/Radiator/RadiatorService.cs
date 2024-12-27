using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Microsoft.Extensions.Logging;
using Repository.Radiator;

namespace Application.Product.Radiator;

internal sealed class RadiatorService : ProductService<RadiatorModel, RadiatorRecord>, IRadiatorService
{
    private readonly IMapper _mapper;
    private readonly IRadiatorRepository _repository;
    private readonly IImageService _imageService;

    public RadiatorService(IRadiatorRepository repository, IMapper mapper, IImageService imageService, ILogger<RadiatorService> logger)
        : base(repository, mapper, imageService, logger)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }
}