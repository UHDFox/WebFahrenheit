namespace Domain.Domain.Entities.Users;

public sealed class FeedbackRecord : IDataObject, IUserFeedback
{
    public FeedbackRecord()
    {
    }

    public FeedbackRecord(Guid userId, string message)
    {
        Message = message;
        UserId = userId;
    }

    public string Message { get; set; } = "";

    public Guid UserId { get; set; }
}