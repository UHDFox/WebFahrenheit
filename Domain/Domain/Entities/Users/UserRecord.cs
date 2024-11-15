using Domain.Domain.Enums;

namespace Domain.Domain.Entities.Users;

public sealed class UserRecord
{
    public UserRecord(string name, string phoneNumber, string passwordHash, string email, UserRole role)
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
    
    public IList<FeedbackRecord> Feedbacks { get; set; } = new List<FeedbackRecord>();
}