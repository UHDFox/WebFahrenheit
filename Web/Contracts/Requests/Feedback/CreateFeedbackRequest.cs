namespace Web.Contracts.Requests.Feedback;

public sealed class CreateFeedbackRequest
{
    public CreateFeedbackRequest(Guid userId, string? email, string message)
    {
        UserId = userId;
        Email = email;
        Message = message;
    }

    public Guid UserId { get; set; }

    public string? Email { get; set; }

    public string Message { get; set; }
}