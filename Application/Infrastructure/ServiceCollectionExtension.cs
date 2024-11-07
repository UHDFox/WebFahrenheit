using Application.Infrastructure.Images;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<IImageService, ImageService>();
    }
}