using Application.Product.Fireplace;
using Application.Product.Pump;
using Application.Product.Radiator;
using Application.Product.WaterBoiler;
using Application.UserFeedback.Feedback;
using Application.UserFeedback.User;
using AutoMapper;
using Contracts.Contracts.CommonResponses;
using Contracts.Contracts.User;
using Domain.Domain.Entities.Products;
using Domain.Domain.Entities.Users;
using Domain.Domain.Enums;

namespace Application.Infrastructure;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<PumpModel, PumpRecord>()
            .ConstructUsing(src => new PumpRecord(
                src.Name,
                src.Article,
                src.Price,
                src.Brand,
                src.Pressure,
                src.PowerSupply,
                src.Description,
                null
            )).ReverseMap();


        CreateMap<FireplaceModel, FireplaceRecord>()
            .ConstructUsing(src => new FireplaceRecord(
                src.Name,
                src.Article,
                src.Price,
                src.FuelUsage,
                src.FireLevel,
                src.Description
            )).ReverseMap();

        CreateMap<WaterBoilerModel, WaterBoilerRecord>()
            .ConstructUsing(src => new WaterBoilerRecord(
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.MaxTemperature,
                src.Description
            )).ReverseMap();

        CreateMap<RadiatorModel, RadiatorRecord>()
            .ConstructUsing(src => new RadiatorRecord(
                src.Name,
                src.Article,
                src.Price,
                src.HeatedValue,
                src.Material,
                src.Description
            )).ReverseMap();
        
        CreateMap<FeedbackModel, FeedbackRecord>()
            .ConstructUsing(src => new FeedbackRecord(src.UserId, src.Message))
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<GetAllResponse<UserResponse>, IReadOnlyCollection<UserModel>>().ReverseMap();
        CreateMap<UserResponse, UserModel>().ReverseMap();
    }
}