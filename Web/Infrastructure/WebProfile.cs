using Application.Pump;
using Application.Pump.Models;
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

public class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<Client, CreateClientRequest>().ReverseMap();
        CreateMap<Client, UpdateClientRequest>().ReverseMap();
        CreateMap<Client, ClientResponse>().ReverseMap();
        CreateMap<Client, UpdatedResponse>().ReverseMap();
        
        CreateMap<Feedback, CreateFeedbackRequest>().ReverseMap();
        CreateMap<Feedback, UpdateFeedbackRequest>().ReverseMap();
        CreateMap<Feedback, FeedbackResponse>().ReverseMap();
        CreateMap<Feedback, UpdatedResponse>().ReverseMap();
        
        
        CreateMap<PumpRecord, CreatePumpRequest>().ReverseMap();
        CreateMap<PumpRecord, UpdatePumpRequest>().ReverseMap();
        CreateMap<PumpRecord, PumpResponse>().ReverseMap();
        CreateMap<PumpRecord, UpdatedResponse>().ReverseMap();
        
        CreateMap<WaterBoilerRecord, CreateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoilerRecord, UpdateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoilerRecord, WaterBoilerResponse>().ReverseMap();
        CreateMap<WaterBoilerRecord, UpdatedResponse>().ReverseMap();
        
        CreateMap<RadiatorRecord, CreateRadiatorRequest>().ReverseMap();
        CreateMap<RadiatorRecord, UpdateRadiatorRequest>().ReverseMap();
        CreateMap<RadiatorRecord, RadiatorResponse>().ReverseMap();
        CreateMap<RadiatorRecord, UpdatedResponse>().ReverseMap();
        
        CreateMap<FireplaceRecord, CreateFireplaceRequest>().ReverseMap();
        CreateMap<FireplaceRecord, UpdateFireplaceRequest>().ReverseMap();
        CreateMap<FireplaceRecord, FireplaceResponse>().ReverseMap();
        CreateMap<FireplaceRecord, UpdatedResponse>().ReverseMap();
        
       /* CreateMap<CreatePumpRequest, PumpRecord>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore())
            .ConstructUsing(src => new PumpRecord(src.Name, src.Price, src.Brand, src.Pressure, src.PowerSupply, null));*/
       CreateMap<CreatePumpRequest, AddPumpModel>();
       CreateMap<GetPumpModel, PumpResponse>();
       CreateMap<UpdatePumpRequest, UpdatePumpModel>();
       CreateMap<UpdatePumpModel, PumpResponse>().ReverseMap();
    }
    
}