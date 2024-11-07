using Microsoft.Extensions.FileProviders;

namespace Web.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void ConfigureStaticFilesUpload(this IApplicationBuilder app)
    {
        var uploadsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        if (!Directory.Exists(uploadsFolderPath))
        {
            Directory.CreateDirectory(uploadsFolderPath);
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(uploadsFolderPath),
            RequestPath = "/uploads"
        });
    }

    public static void ConfigureCORSPolicy(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "SomePolicy", policyBuilder =>
            {
                policyBuilder.AllowAnyHeader();
                policyBuilder.AllowAnyMethod();
                policyBuilder.AllowAnyOrigin();
            });
        });
    }
}