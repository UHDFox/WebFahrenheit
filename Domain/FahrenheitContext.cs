using Domain.Domain.Entities.Products;
using Domain.Domain.Entities.Users;
using Domain.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class FahrenheitContext : DbContext
{
    public FahrenheitContext(DbContextOptions opts) : base(opts)
    {
        
    }

    public DbSet<UserRecord> Users => Set<UserRecord>();
    
    public DbSet<FeedbackRecord> Feedbacks => Set<FeedbackRecord>();
    
    public DbSet<FireplaceRecord> Fireplaces => Set<FireplaceRecord>();
    
    public DbSet<PumpRecord> Pumps => Set<PumpRecord>();
    
    public DbSet<RadiatorRecord> Radiators => Set<RadiatorRecord>();
    
    public DbSet<WaterBoilerRecord> WaterBoilers => Set<WaterBoilerRecord>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<UserRole>();

        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}