using Microsoft.Extensions.FileProviders;
using Serilog;

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

    public static void AddSerilog(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Async(a => a.Console(
                outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"))
            .WriteTo.Async(a => a.File("/app/logs/log.txt", rollingInterval: RollingInterval.Day)) // Write to a file in /app/logs
            .CreateLogger();
        
        services.AddSingleton(Log.Logger);
    }
}