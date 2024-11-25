using Application.Product.Fireplace;
using Application.Product.Pump;
using Application.Product.Radiator;
using Application.Product.WaterBoiler;
using Application.UserFeedback.User;
using AutoMapper;
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

        // User mappings
        CreateMap<UserModel, UserRecord>()
            .ConstructUsing(src =>
                new UserRecord(src.Name, src.Password, src.Email, src.PhoneNumber, UserRole.User));

        CreateMap<UserRecord, UserModel>()
            .ConstructUsing(src =>
                new UserModel(
                    src.Id,
                    src.Name,
                    src.PasswordHash,
                    src.Email,
                    src.PhoneNumber,
                    src.Role
                ));

        CreateMap<RegisterModel, UserModel>()
            .ConstructUsing(src =>
                new UserModel(
                    Guid.NewGuid(),
                    src.Name,
                    src.Password,
                    src.Email,
                    src.PhoneNumber,
                    UserRole.User
                ))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}