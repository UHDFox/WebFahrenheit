using Microsoft.Extensions.DependencyInjection;
using Repository.Feedback;
using Repository.Fireplace;
using Repository.Pump;
using Repository.Radiator;
using Repository.User;
using Repository.WaterBoiler;

namespace Repository.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IFireplaceRepository, FireplaceRepository>();
        services.AddTransient<IWaterBoilerRepository, WaterBoilerRepository>();
        services.AddTransient<IRadiatorRepository, RadiatorRepository>();
        services.AddTransient<IPumpRepository, PumpRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IFeedbackRepository, FeedbackRepository>();


        return services;
    }
}
