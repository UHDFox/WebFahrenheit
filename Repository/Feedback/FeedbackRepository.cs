using Domain;
using Domain.Domain.Entities.Users;

namespace Repository.Feedback;

internal sealed class FeedbackRepository : Repository<FeedbackRecord>, IFeedbackRepository
{
    public FeedbackRepository(FahrenheitContext context) : base(context)
    {
    }
}