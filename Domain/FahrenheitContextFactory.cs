
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Domain;

public class FahrenheitContextFactory : IDesignTimeDbContextFactory<FahrenheitContext>
{
    public FahrenheitContext CreateDbContext(string[] args)
    {
        // Получаем текущий путь
        var basePath = Directory.GetCurrentDirectory();

        // Строим конфигурацию
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath) // Устанавливаем базовый путь
            .AddJsonFile("appsettings.json") // Подключаем файл appsettings.json
            .Build();

        // Создаем DbContextOptions
        var optionsBuilder = new DbContextOptionsBuilder<FahrenheitContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("Psql"));

        return new FahrenheitContext(optionsBuilder.Options);
    }
}