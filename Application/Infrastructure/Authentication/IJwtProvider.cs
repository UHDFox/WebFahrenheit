using Domain.Domain.Entities.Users;

namespace Application.Infrastructure.Authentication;

public interface IJwtProvider
{
    public string GenerateToken(UserRecord user);
}
