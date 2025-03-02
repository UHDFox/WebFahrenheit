using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Microsoft.Extensions.Logging;
using Repository.Fireplace;

namespace Application.Product.Fireplace;

internal sealed class FireplaceService : ProductService<FireplaceModel, FireplaceRecord>, IFireplaceService
{
    private readonly IMapper _mapper;
    private readonly IFireplaceRepository _repository;
    private readonly IImageService _imageService;

    public FireplaceService(IFireplaceRepository repository, IMapper mapper, IImageService imageService, ILogger<FireplaceService> logger) 
        : base(repository, mapper, imageService, logger)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }
}