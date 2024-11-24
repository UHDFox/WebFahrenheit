using Domain;
using Domain.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class Repository<T> : IRepository<T> where T: IDataObject
{
    protected FahrenheitContext _context;
    
    public Repository(FahrenheitContext context)
    {
        _context = context;
    }
    public async Task<IReadOnlyCollection<T>> GetAllAsync(int offset, int limit) =>
        await _context.Set<T>().Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await _context.Set<T>().CountAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(T data)
    {
        await _context.Set<T>().AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(T data) => _context.Set<T>().Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        _context.Set<T>().Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}