namespace Domain.Domain.Entities.Users;

public sealed class FeedbackRecord
{
    public FeedbackRecord(){}

    public FeedbackRecord(string message, Guid userId)
    {
        Message = message;
        UserId = userId;
    }

    public Guid Id { get; set; }

    public string Message { get; set; } = "";
    
    public Guid UserId { get; set; }
    
    public UserRecord? User { get; set; }
}