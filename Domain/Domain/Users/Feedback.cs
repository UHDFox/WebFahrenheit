namespace Domain.Domain.Users;

public sealed class Feedback
{
    public Feedback(string message, Guid clientId)
    {
        Message = message;
        ClientId = clientId;
    }
    public Guid Id { get; set; }
    
    public string Message { get; set; }
    
    public Guid ClientId { get; set; }
    
    public Client? Client { get; set; }
}