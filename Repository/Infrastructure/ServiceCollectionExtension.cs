using Microsoft.Extensions.DependencyInjection;
using Repository.Pump;

namespace Repository.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IPumpRepository, PumpRepository>();
    }
}