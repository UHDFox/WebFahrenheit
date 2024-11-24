using Domain;
using Domain.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Repository.User;

public sealed class UserRepository : Repository<UserRecord>, IUserRepository
{
    public UserRepository(FahrenheitContext context) : base(context)
    {
    }

    public Task<UserRecord?> GetByEmailAsync(string email)
        => _context.Users.FirstOrDefaultAsync(e => e.Email == email);
}