using Application.Pump;
using Application.Pump.Models;
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
    }
}