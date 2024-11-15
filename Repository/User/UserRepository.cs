using Domain;
using Domain.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Repository.User;

public sealed class UserRepository : IUserRepository
{
    private readonly FahrenheitContext context;

    public UserRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<UserRecord>> GetAllAsync(int offset, int limit)
    {
        return await context.Users.Skip(offset).Take(limit).ToListAsync();
    }

    public async Task<int> GetTotalAmountAsync()
    {
        return await context.Users.CountAsync();
    }

    public async Task<UserRecord?> GetByIdAsync(Guid id)
    {
        return await context.Users
            .AsNoTracking()
            .Include(x => x.Feedbacks)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Guid> AddAsync(UserRecord data)
    {
        var result = await context.Users.AddAsync(data);
        await SaveChangesAsync();
        return result.Entity.Id;
    }

    public void Update(UserRecord data)
    { ;
        context.Users.Update(data);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Users.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public Task<UserRecord?> GetByEmailAsync(string email)
        => context.Users.FirstOrDefaultAsync(e => e.Email == email);
}
