using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Infrastructure.Authentication;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtBearerOptions options;

    public JwtProvider(IOptions<JwtBearerOptions> options)
    {
        this.options = options.Value;
    }

    public string GenerateToken(UserRecord user)
    {
        var claims = new List<Claim> {new Claim(ClaimTypes.Role, user.Role.ToString()), new Claim( ClaimTypes.NameIdentifier, user.Id.ToString()) };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}
