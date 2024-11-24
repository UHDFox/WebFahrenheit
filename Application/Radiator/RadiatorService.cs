using Application.Infrastructure.Images;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Repository.Radiator;


namespace Application.Radiator;

internal sealed class RadiatorService : ProductService<RadiatorModel, RadiatorRecord>, IRadiatorService
{
    private readonly IMapper _mapper;
    private readonly IRadiatorRepository _repository;
    private readonly IImageService _imageService;

    public RadiatorService(IRadiatorRepository repository, IMapper mapper, IImageService imageService)
        : base(repository, mapper, imageService)
    {
        _repository = repository;
        _mapper = mapper;
        _imageService = imageService;
    }
}