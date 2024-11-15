using Domain;
using Domain.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Repository.FeedbackRepository;

namespace Repository.Feedback;

internal sealed class FeedbackRepository : IFeedbackRepository
{
    private readonly FahrenheitContext context;
    
    public FeedbackRepository(FahrenheitContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<FeedbackRecord>> GetAllAsync(int offset, int limit) =>
        await context.Feedbacks.Skip(offset).Take(limit).ToListAsync();

    public async Task<int> GetTotalAmountAsync() => await context.Feedbacks.CountAsync();

    public async Task<FeedbackRecord?> GetByIdAsync(Guid id) => await context.Feedbacks.AsNoTracking().FirstOrDefaultAsync(record => record.Id == id);

    public async Task<Guid> AddAsync(FeedbackRecord data)
    {
        await context.Feedbacks.AddAsync(data);
        await SaveChangesAsync();
        return data.Id;
    }

    public void Update(FeedbackRecord data) => context.Feedbacks.Update(data);

    public async Task<bool> DeleteAsync(Guid id)
    {
        context.Feedbacks.Remove((await GetByIdAsync(id))!);
        return await SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
}