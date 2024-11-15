namespace Application.Feedback;

public sealed class AddFeedbackModel
{
    public AddFeedbackModel()
    {
        
    }
    
    public AddFeedbackModel(Guid clientId, string?  email, string message)
    {        
        ClientId = clientId;
        Email = email;
        Message = message;
    }
    public Guid ClientId { get; set; }

    public string? Email { get; set; }

    public string Message { get; set; } = "";
}