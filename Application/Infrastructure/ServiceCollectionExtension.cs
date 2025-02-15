using Application.Infrastructure.Authentication;
using Application.Infrastructure.Images;
using Application.Product.Fireplace;
using Application.Product.Pump;
using Application.Product.Radiator;
using Application.Product.WaterBoiler;
using Application.UserFeedback;
using Application.UserFeedback.Feedback;
using Application.UserFeedback.User;
using Domain.Domain.Entities;
using FahrenheitAuthService.Client;
using FahrenheitAuthService.Client.Implemetations;
using FahrenheitAuthService.Client.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IFeedbackService, FeedbackService>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IRadiatorService, RadiatorService>();
        services.AddTransient<IPumpService, PumpService>();
        services.AddTransient<IFireplaceService, FireplaceService>();
        services.AddTransient<IWaterBoilerService, WaterBoilerService>();
        services.AddTransient<IUserService, UserService>();

        // Register AuthClient directly with HttpClient
        services.Configure<AuthServiceOptions>(configuration.GetSection("AuthServiceOptions"));

        // Регистрация HttpClient и AuthClient
        services.AddHttpClient<IAuthClient, AuthClient>((serviceProvider, client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<AuthServiceOptions>>().Value;
            client.BaseAddress = new Uri(options.Uri);
        });
        
        services.AddTransient<AuthClient>(x => x.GetRequiredService<AuthClient>());

        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordProvider, PasswordProvider>();

        services.AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }

}