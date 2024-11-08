using Domain;
using Domain.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Repository.WaterBoiler;

internal sealed class WaterBoilerRepository : IWaterBoilerRepository
{
    private readonly FahrenheitContext context;


    public WaterBoilerRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<WaterBoilerRecord>> GetAllAsync(int offset, int limit) =>
        await context.WaterBoilers.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.WaterBoilers.CountAsync();

    public async Task<WaterBoilerRecord?> GetByIdAsync(Guid id) => await context.WaterBoilers.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(WaterBoilerRecord data)
    {
        await context.WaterBoilers.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(WaterBoilerRecord data) => context.WaterBoilers.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.WaterBoilers.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}