namespace Application.Feedback.Models;

public sealed class GetFeedbackModel
{
    public GetFeedbackModel(Guid id,  string? email, string message, Guid clientId)
    {
        Id = id;
        Email = email;
        Message = message;
        ClientId = clientId;
    }
    public Guid Id { get; set; }
    public string? Email { get; set; }

    public string Message { get; set; }
    
    public Guid ClientId { get; set; }
}