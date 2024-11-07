namespace Web.Contracts.Requests.Client;

public sealed class ClientResponse
{
    public ClientResponse(Guid id, string name, string phoneNumber, string mail, string password)
    {
        Id = id;
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
}