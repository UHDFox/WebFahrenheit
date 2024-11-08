using Domain;
using Domain.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Repository.Radiator;

internal sealed class RadiatorRepository : IRadiatorRepository
{
    private readonly FahrenheitContext context;


    public RadiatorRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<RadiatorRecord>> GetAllAsync(int offset, int limit) =>
        await context.Radiators.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Radiators.CountAsync();

    public async Task<RadiatorRecord?> GetByIdAsync(Guid id) => await context.Radiators.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(RadiatorRecord data)
    {
        await context.Radiators.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(RadiatorRecord data) => context.Radiators.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Radiators.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}