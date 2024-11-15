using Domain;
using Domain.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Repository.Fireplace;

internal sealed class FireplaceRepository : IFireplaceRepository
{
    private readonly FahrenheitContext context;
    
    public FireplaceRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<FireplaceRecord>> GetAllAsync(int offset, int limit) =>
        await context.Fireplaces.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Fireplaces.CountAsync();

    public async Task<FireplaceRecord?> GetByIdAsync(Guid id) => await context.Fireplaces.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(FireplaceRecord data)
    {
        await context.Fireplaces.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(FireplaceRecord data) => context.Fireplaces.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Fireplaces.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}
