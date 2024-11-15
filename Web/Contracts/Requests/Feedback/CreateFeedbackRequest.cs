namespace Web.Contracts.Requests.Feedback;

public sealed class CreateFeedbackRequest
{
    public CreateFeedbackRequest(Guid clientId, string?  email, string message)
    {        
        ClientId = clientId;
        Email = email;
        Message = message;
    }
    public Guid ClientId { get; set; }

    public string? Email { get; set; }

    public string Message { get; set; }
}