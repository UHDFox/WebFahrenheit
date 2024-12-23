using Application.Product.Fireplace;
using Application.Product.Pump;
using Application.Product.Radiator;
using Application.Product.WaterBoiler;
using Application.UserFeedback.Feedback;
using Application.UserFeedback.User;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Domain.Domain.Entities.Users;
using Domain.Domain.Enums;
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
        CreateMap<UserModel, UserResponse>()
            .ConstructUsing(src => new UserResponse(
                src.Id,
                src.Name,
                src.PhoneNumber,
                src.Email,
                src.Password,
                src.Role));

        CreateMap<CreateUserRequest, UserModel>()
            .ConstructUsing(src => new UserModel(
                Guid.NewGuid(),
                src.Name,
                src.Password,
                src.Email,
                src.PhoneNumber,
                src.Role
            ));


        CreateMap<UpdateUserRequest, UserModel>()
            .ConstructUsing(src =>
                new UserModel(
                    src.Id,
                    src.Name,
                    src.Password,
                    src.Email,
                    src.PhoneNumber,
                    src.Role
                )).ReverseMap();
        CreateMap<LoginRequest, LoginModel>();
        
        CreateMap<RegisterRequest, RegisterModel>();
        
        CreateMap<RegisterModel, UserModel>()
            .ConstructUsing(src =>
                new UserModel(
                    Guid.Empty,
                    src.Name,
                    src.Password,
                    src.Email,
                    src.PhoneNumber,
                    UserRole.User
                ));

        CreateMap<CreateFeedbackRequest, FeedbackModel>();
        CreateMap<FeedbackModel, FeedbackResponse>();
        CreateMap<FeedbackRecord, FeedbackModel>()
            .ConstructUsing(src => new FeedbackModel(src.Id, new string(""), src.Message, src.UserId));

        // Pump mappings
        CreateMap<CreatePumpRequest, PumpModel>()
            .ConstructUsing(src => new PumpModel(
                Guid.NewGuid(),
                src.Name,
                src.Article,
                src.Price,
                src.Brand,
                src.Pressure,
                src.Description,
                src.PowerSupply,
                null
            ));
        CreateMap<PumpModel, PumpResponse>()
            .ConstructUsing(src => new PumpResponse(
                src.Id, src.Name, src.Article, src.Price, src.Brand, src.Pressure, src.PowerSupply, src.Description,
                src.ImagePath ?? String.Empty));

        CreateMap<UpdatePumpRequest, PumpModel>()
            .ConstructUsing(request => new PumpModel(
                request.Id,
                request.Name ?? string.Empty,
                request.Article,
                request.Price,
                request.Brand ?? string.Empty,
                request.Pressure,
                request.Description,
                request.PowerSupply,
                null
            ));

        CreateMap<WaterBoilerRecord, CreateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoilerRecord, UpdateWaterBoilerRequest>().ReverseMap();
        CreateMap<WaterBoilerRecord, WaterBoilerResponse>().ReverseMap();
        CreateMap<WaterBoilerRecord, UpdatedResponse>().ReverseMap();

        CreateMap<RadiatorRecord, CreateRadiatorRequest>().ReverseMap();
        CreateMap<RadiatorRecord, UpdateRadiatorRequest>().ReverseMap();
        CreateMap<RadiatorRecord, RadiatorResponse>().ReverseMap();
        CreateMap<RadiatorRecord, UpdatedResponse>().ReverseMap();

        // Fireplace mappings
        CreateMap<CreateFireplaceRequest, FireplaceModel>()
            .ConstructUsing(src => new FireplaceModel(
                Guid.NewGuid(),
                src.Name,
                src.Article,
                src.Price,
                src.FuelUsage,
                src.FireLevel,
                src.Description,
                null
            ));
        CreateMap<FireplaceModel, FireplaceResponse>()
            .ConstructUsing(src => new FireplaceResponse(
                src.Id, src.Name, src.Article, src.Price, src.FuelUsage, src.FireLevel, src.Description,
                src.ImagePath ?? String.Empty));

        CreateMap<UpdateFireplaceRequest, FireplaceModel>()
            .ConstructUsing(src => new FireplaceModel(
                src.Id,
                src.Name,
                src.Article,
                src.Price,
                src.FuelUsage,
                src.FireLevel,
                src.Description,
                null
            ));

        // WaterBoiler mappings
        CreateMap<CreateWaterBoilerRequest, WaterBoilerModel>()
            .ConstructUsing(src => new WaterBoilerModel(
                Guid.NewGuid(),
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.MaxTemperature,
                src.Description,
                null
            ));
        CreateMap<WaterBoilerModel, WaterBoilerResponse>()
            .ConstructUsing(src => new WaterBoilerResponse(
                src.Id, src.Name, src.Article, src.Price, src.HeatedValue, src.Material, src.Description,
                src.ImagePath));

        CreateMap<UpdateWaterBoilerRequest, WaterBoilerModel>()
            .ConstructUsing(src => new WaterBoilerModel(
                src.Id,
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.MaxTemperature,
                src.Description,
                null
            ));

        // Radiator mappings
        CreateMap<CreateRadiatorRequest, RadiatorModel>()
            .ConstructUsing(src => new RadiatorModel(
                Guid.NewGuid(),
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.Description,
                null
            ));
        CreateMap<RadiatorModel, RadiatorResponse>()
            .ConstructUsing(src => new RadiatorResponse(
                src.Id, src.Name, src.Article, src.Price, src.HeatedValue, src.Material, src.Description,
                src.ImagePath));

        CreateMap<UpdateRadiatorRequest, RadiatorModel>()
            .ConstructUsing(src => new RadiatorModel(
                src.Id,
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.Description,
                null
            ));
    }
}