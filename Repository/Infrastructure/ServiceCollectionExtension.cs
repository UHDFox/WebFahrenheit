using Microsoft.Extensions.DependencyInjection;
using Repository.Fireplace;
using Repository.Pump;
using Repository.WaterBoiler;

namespace Repository.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPumpRepository, PumpRepository>();
        
        services.AddTransient<IFireplaceRepository, FireplaceRepository>();
        
        services.AddTransient<IWaterBoilerRepository, WaterBoilerRepository>();
    }
}