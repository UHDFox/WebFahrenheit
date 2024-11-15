using Application.Fireplace.Models;
using Application.Pump;
using Application.Pump.Models;
using Application.Radiator.Models;
using Application.User;
using Application.WaterBoiler.Models;
using AutoMapper;
using Domain.Domain.Entities.Products;
using Domain.Domain.Entities.Users;
using Domain.Domain.Enums;

namespace Application.Infrastructure;

public class ApplicationProfile : Profile
{
    public ApplicationProfile()
    {
        CreateMap<UpdatePumpModel, PumpRecord>().ReverseMap();
        CreateMap<GetPumpModel, PumpRecord>().ReverseMap();
        CreateMap<AddPumpModel, PumpRecord>()
            .ConstructUsing(src => new PumpRecord(
                src.Name, 
                src.Article,
                src.Price, 
                src.Brand, 
                src.Pressure, 
                src.PowerSupply,
                src.Description,
                null
            ));
        
        CreateMap<UpdateFireplaceModel, FireplaceRecord>().ReverseMap();
        CreateMap<GetFireplaceModel, FireplaceRecord>().ReverseMap();
        CreateMap<AddFireplaceModel, FireplaceRecord>();
        
        CreateMap<UpdateWaterBoilerModel, WaterBoilerRecord>().ReverseMap();
        CreateMap<GetWaterBoilerModel, WaterBoilerRecord>().ReverseMap();
        CreateMap<AddWaterBoilerModel, WaterBoilerRecord>();
        
        CreateMap<UpdateRadiatorModel, RadiatorRecord>().ReverseMap();
        CreateMap<GetRadiatorModel, RadiatorRecord>().ReverseMap();
        CreateMap<AddRadiatorModel, RadiatorRecord>();
        
        
        CreateMap<GetUserModel, UserRecord>().ReverseMap();
        CreateMap<AddUserModel, UserRecord>()
            .ForCtorParam("passwordHash", opt =>
                opt.MapFrom(src => src.Password));
        CreateMap<UpdateUserModel, UserRecord>().ReverseMap();
        CreateMap<RegisterModel, AddUserModel>()
            .ConstructUsing(src => new AddUserModel(src.Name, src.Password, src.Email, src.PhoneNumber, UserRole.User));
    }
}