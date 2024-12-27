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
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Warning)
            .WriteTo.Async(a => a.Console(
                outputTemplate: "{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}"))
            .WriteTo.Async(a => a.File("/app/logs/log.txt", rollingInterval: RollingInterval.Day))
            .CreateLogger();

        services.AddLogging(logging =>
        {
            logging.AddSerilog();
            
            logging.AddFilter("Microsoft.AspNetCore", LogLevel.Warning); 
            logging.AddFilter("Microsoft.AspNetCore.Hosting", LogLevel.Warning); 
            logging.AddFilter("Microsoft.AspNetCore.DataProtection", LogLevel.Error); // Suppress data protection warnings
            logging.AddFilter("Microsoft.AspNetCore.Diagnostics", LogLevel.Warning);
            logging.AddFilter("Microsoft.AspNetCore.Routing", LogLevel.Warning); 
            logging.AddFilter("Microsoft.AspNetCore.Mvc", LogLevel.Warning); 
            
            logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning); 
            logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); 
        });
    }
    
}