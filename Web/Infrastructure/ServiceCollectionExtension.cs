using System.Net;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.FileProviders;
using Serilog;

namespace Web.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void AddFahrenheitDbContext(this IServiceCollection services)
    {
        services.AddDbContext<FahrenheitContext>((provider, builder) =>
        {
            builder.UseNpgsql(provider.GetRequiredService<IConfiguration>().GetConnectionString("Psql"));
            builder.ConfigureWarnings(
                warnings => warnings.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
        });
    }
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

    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var dbContext = services.GetRequiredService<FahrenheitContext>();
                dbContext.Database.Migrate(); 
                Log.Logger.Information("Database migration applied successfully.");
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"An error occurred while applying the database migration: {ex.Message}");
            }
        }
    }
}