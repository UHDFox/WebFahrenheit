using Domain;
using Domain.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Repository.Pump;

internal sealed class PumpRepository : IPumpRepository
{
    private readonly FahrenheitContext context;


    public PumpRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<PumpRecord>> GetAllAsync(int offset, int limit) =>
        await context.Pumps.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Pumps.CountAsync();

    public async Task<PumpRecord?> GetByIdAsync(Guid id) => await context.Pumps.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(PumpRecord data)
    {
        await context.Pumps.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(PumpRecord data) => context.Pumps.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Pumps.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}