using Domain.Domain.Enums;

namespace Application.User;

public sealed class UserModel : CustomerItem
{
    public UserModel(Guid id, string name, string password, string email, string phoneNumber, UserRole role) : base(id)
    {
        Id = id;
        Name = name;
        Password = password;
        Email = email;
        PhoneNumber = phoneNumber;
        Role = role;
    }

    public string Name { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }

    public UserRole Role { get; set; }
}