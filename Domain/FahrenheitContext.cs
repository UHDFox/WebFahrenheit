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
    
    public DbSet<FireplaceRecord> Fireplaces => Set<FireplaceRecord>();
    
    public DbSet<PumpRecord> Pumps => Set<PumpRecord>();
    
    public DbSet<RadiatorRecord> Radiators => Set<RadiatorRecord>();
    
    public DbSet<WaterBoilerRecord> WaterBoilers => Set<WaterBoilerRecord>();
}