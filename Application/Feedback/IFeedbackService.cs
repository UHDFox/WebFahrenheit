using Application.Feedback.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Feedback;

public interface IFeedbackService
{
    public string ReturnNameFromToken();

    public Task<Guid> AddUsingEmail(AddFeedbackModel model);

    Task<IReadOnlyCollection<GetFeedbackModel>> GetListAsync(int offset, int limit);

    Task<GetFeedbackModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddFeedbackModel model);

    Task<UpdateFeedbackModel> UpdateAsync(UpdateFeedbackModel model, IFormFile? imageFile);

    Task<bool> DeleteAsync(Guid id);
}