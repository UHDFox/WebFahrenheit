using AutoMapper;
using Domain.Domain.Products;
using Domain.Domain.Users;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Client;
using Web.Contracts.Requests.Feedback;
using Web.Contracts.Requests.Fireplace;
using Web.Contracts.Requests.Pump;
using Web.Contracts.Requests.Radiator;
using Web.Contracts.Requests.Waterboiler;

namespace Web.Infrastructure;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Client, CreateClientRequest>().ReverseMap();
        CreateMap<Client, UpdateClientRequest>().ReverseMap();
        CreateMap<Client, ClientResponse>().ReverseMap();
        CreateMap<Client, UpdatedResponse>().ReverseMap();
        
        CreateMap<Feedback, CreateFeedbackRequest>().ReverseMap();
        CreateMap<Feedback, UpdateFeedbackRequest>().ReverseMap();
        CreateMap<Feedback, FeedbackResponse>().ReverseMap();
        CreateMap<Feedback, UpdatedResponse>().ReverseMap();
        
        
        CreateMap<Pump, CreatePumpRequest>().ReverseMap();
        CreateMap<Pump, UpdatePumpRequest>().ReverseMap();
        CreateMap<Pump, PumpResponse>().ReverseMap();
        CreateMap<Pump, UpdatedResponse>().ReverseMap();
        
        CreateMap<WaterBoiler, CreateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoiler, UpdateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoiler, WaterBoilerResponse>().ReverseMap();
        CreateMap<WaterBoiler, UpdatedResponse>().ReverseMap();
        
        CreateMap<Radiator, CreateRadiatorRequest>().ReverseMap();
        CreateMap<Radiator, UpdateRadiatorRequest>().ReverseMap();
        CreateMap<Radiator, RadiatorResponse>().ReverseMap();
        CreateMap<Radiator, UpdatedResponse>().ReverseMap();
        
        CreateMap<Fireplace, CreateFireplaceRequest>().ReverseMap();
        CreateMap<Fireplace, UpdateFireplaceRequest>().ReverseMap();
        CreateMap<Fireplace, FireplaceResponse>().ReverseMap();
        CreateMap<Fireplace, UpdatedResponse>().ReverseMap();
        
        CreateMap<CreatePumpRequest, Pump>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ConstructUsing(src => new Pump(src.Name, src.Price, src.Brand, src.Pressure, src.PowerSupply, null));
    }
    
}