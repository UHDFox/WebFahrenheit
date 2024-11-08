using Application.Fireplace.Models;
using Application.Pump;
using Application.Pump.Models;
using Application.Radiator.Models;
using Application.WaterBoiler.Models;
using AutoMapper;
using Domain.Domain.Products;

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
                src.Price, 
                src.Brand, 
                src.Pressure, 
                src.PowerSupply,
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
    }
}