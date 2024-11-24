namespace Application.Feedback;

public sealed class FeedbackModel : CustomerItem
{
    public FeedbackModel(Guid id, string? email, string message, Guid userId) : base(id)
    {
        Id = id;
        Email = email;
        Message = message;
        UserId = userId;
    }
    public string? Email { get; set; }

    public string Message { get; set; }
    
    public Guid UserId { get; set; }
}