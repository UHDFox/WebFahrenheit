using Application.Infrastructure.Authentication;
using Application.Infrastructure.Images;
using Application.Product.Fireplace;
using Application.Product.Pump;
using Application.Product.Radiator;
using Application.Product.WaterBoiler;
using Application.UserFeedback.Feedback;
using Application.UserFeedback.User;
using Microsoft.Extensions.DependencyInjection;


namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        /*services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IPumpService, PumpService>();
        services.AddTransient<IFireplaceService, FireplaceService>();
        services.AddTransient<IWaterBoilerService, WaterBoilerService>();
        services.AddTransient<IRadiatorService, RadiatorService>();

        services.AddTransient<IUserService, UserService>();*/

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IFeedbackService, FeedbackService>();
        services.AddTransient<IImageService, ImageService>();
        services.AddTransient<IRadiatorService, RadiatorService>();
        services.AddTransient<IPumpService, PumpService>();
        services.AddTransient<IFireplaceService, FireplaceService>();
        services.AddTransient<IWaterBoilerService, WaterBoilerService>();


        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordProvider, PasswordProvider>();

        services.AddHttpContextAccessor();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}