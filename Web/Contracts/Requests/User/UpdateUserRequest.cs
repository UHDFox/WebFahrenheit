using Domain.Domain.Enums;

namespace Web.Contracts.Requests.User;

public sealed class UpdateUserRequest
{
    public UpdateUserRequest(string name, string phoneNumber, string passwordHash, string email, UserRole role)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        Email = email;
        Role = role;
    }

    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }
}