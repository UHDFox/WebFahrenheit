using Domain.Domain;
using Domain.Domain.Products;
using Domain.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class FahrenheitContext : DbContext
{
    public FahrenheitContext(DbContextOptions opts) : base(opts)
    {
        
    }

    public DbSet<Client> Clients => Set<Client>();
    
    public DbSet<Feedback> Feedbacks => Set<Feedback>();
    
    public DbSet<Fireplace> Fireplaces => Set<Fireplace>();
    
    public DbSet<Pump> Pumps => Set<Pump>();
    
    public DbSet<Radiator> Radiators => Set<Radiator>();
    
    public DbSet<WaterBoiler> WaterBoilers => Set<WaterBoiler>();
}