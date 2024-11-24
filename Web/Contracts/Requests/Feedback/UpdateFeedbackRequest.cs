namespace Web.Contracts.Requests.Feedback;

public sealed class UpdateFeedbackRequest
{
    public UpdateFeedbackRequest(Guid id, string message, Guid userId)
    {
        Id = id;
        Message = message;
        UserId = userId;
    }
    public Guid Id { get; set; }
    
    public string Message { get; set; }
    
    public Guid UserId { get; set; }
}