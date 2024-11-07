namespace Domain.Domain.Users;

public class Client
{
    public Client(string name, string phoneNumber, string mail, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Mail = mail;
        Password = password;
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Mail { get; set; }
    
    public string Password { get; set; }

    public IList<Feedback> Feedbacks { get; set; } = new List<Feedback>();
}