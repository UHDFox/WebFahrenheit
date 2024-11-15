using Domain.Domain.Enums;

namespace Application.User;

public sealed class UpdateUserModel
{
    public UpdateUserModel(string name, string phoneNumber, string passwordHash, string email, UserRole role)
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


