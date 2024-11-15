using Application.Fireplace.Models;
using Application.Pump.Models;
using Application.Radiator.Models;
using Application.User;
using Application.WaterBoiler.Models;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.Data;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Feedback;
using Web.Contracts.Requests.Fireplace;
using Web.Contracts.Requests.Fireplace.Requests;
using Web.Contracts.Requests.Pump;
using Web.Contracts.Requests.Radiator;
using Web.Contracts.Requests.Radiator.Requests;
using Web.Contracts.Requests.User;
using Web.Contracts.Requests.Waterboiler;
using Web.Contracts.Requests.Waterboiler.Requests;
using LoginRequest = Web.Contracts.Requests.User.LoginRequest;
using RegisterRequest = Web.Contracts.Requests.User.RegisterRequest;

namespace Web.Infrastructure;

public class WebProfile : Profile
{
    public WebProfile()
    {
        CreateMap<GetUserModel, UserResponse>().ReverseMap();
        CreateMap<CreateUserRequest, AddUserModel>().ReverseMap();
        CreateMap<UpdateUserRequest, UpdateUserModel>().ReverseMap();
        CreateMap<LoginRequest, LoginModel>();
        CreateMap<RegisterRequest, RegisterModel>();
        
        CreateMap<FeedbackRecord, CreateFeedbackRequest>().ReverseMap();
        CreateMap<FeedbackRecord, UpdateFeedbackRequest>().ReverseMap();
        CreateMap<FeedbackRecord, FeedbackResponse>().ReverseMap();
        CreateMap<FeedbackRecord, UpdatedResponse>().ReverseMap();
        
        
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
       
       CreateMap<CreateFireplaceRequest, AddFireplaceModel>();
       CreateMap<GetFireplaceModel, FireplaceResponse>();
       CreateMap<UpdateFireplaceRequest, UpdateFireplaceModel>();
       CreateMap<UpdateFireplaceModel, FireplaceResponse>().ReverseMap();
       
       CreateMap<CreateWaterBoilerRequest, AddWaterBoilerModel>();
       CreateMap<GetWaterBoilerModel, WaterBoilerResponse>();
       CreateMap<UpdateWaterBoilerRequest, UpdateWaterBoilerModel>();
       CreateMap<UpdateWaterBoilerModel, WaterBoilerResponse>().ReverseMap();
       
       CreateMap<CreateRadiatorRequest, AddRadiatorModel>();
       CreateMap<GetRadiatorModel, RadiatorResponse>();
       CreateMap<UpdateRadiatorRequest, UpdateRadiatorModel>();
       CreateMap<UpdateRadiatorModel, RadiatorResponse>().ReverseMap();
       CreateMap<CreateFeedbackRequest, FeedbackRecord>().ReverseMap();
    }
}