namespace Web.Contracts.Requests.Feedback;

public class CreateFeedbackRequest
{
    public CreateFeedbackRequest(string message, Guid clientId)
    {
        Message = message;
        ClientId = clientId;
    }
    public string Message { get; set; }
    
    public Guid ClientId { get; set; }
}