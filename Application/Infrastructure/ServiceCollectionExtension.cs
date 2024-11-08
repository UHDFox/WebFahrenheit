using Application.Fireplace;
using Application.Infrastructure.Images;
using Application.Pump;
using Application.WaterBoiler;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IPumpService, PumpService>();
        services.AddTransient<IFireplaceService, FireplaceService>();
        services.AddTransient<IWaterBoilerService, WaterBoilerService>();
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}