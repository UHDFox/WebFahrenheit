namespace Web.Contracts.Requests.Feedback;

public class FeedbackResponse
{
    public FeedbackResponse(Guid id, string message, Guid clientId)
    {
        Id = id;
        Message = message;
        ClientId = clientId;
    }
    public Guid Id { get; set; }
    
    public string Message { get; set; }
    
    public Guid ClientId { get; set; }
}