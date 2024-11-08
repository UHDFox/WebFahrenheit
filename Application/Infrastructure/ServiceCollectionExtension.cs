using Application.Fireplace;
using Application.Infrastructure.Images;
using Application.Pump;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IPumpService, PumpService>();
        services.AddTransient<IFireplaceService, FireplaceService>();
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}