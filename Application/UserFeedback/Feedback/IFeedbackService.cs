namespace Application.UserFeedback.Feedback;

public interface IFeedbackService : ICustomerService<FeedbackModel>
{
    public Task<Guid> AddUsingEmail(FeedbackModel model);
}