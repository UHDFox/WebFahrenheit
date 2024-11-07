namespace Web.Contracts.Requests.Client;

public class CreateClientRequest
{
    public CreateClientRequest(string name, string phoneNumber, string mail, string password)
    {
        Name = name;
        PhoneNumber = phoneNumber;
        Mail = mail;
        Password = password;
    }
    public string Name { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Mail { get; set; }
    
    public string Password { get; set; }
}