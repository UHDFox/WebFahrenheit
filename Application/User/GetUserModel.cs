using Domain.Domain.Enums;

namespace Application.User;

public sealed class GetUserModel
{
    public GetUserModel(Guid id, string name, string phoneNumber, string email, string passwordHash, UserRole role)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        PasswordHash = passwordHash;
        Role = role;
    }

    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public UserRole Role { get; set; }
}
