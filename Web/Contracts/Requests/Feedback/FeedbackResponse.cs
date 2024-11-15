namespace Web.Contracts.Requests.Feedback;

public class FeedbackResponse
{
    public Guid Id { get; set; }

    public string Message { get; set; } = "";
    
    public Guid ClientId { get; set; }
}