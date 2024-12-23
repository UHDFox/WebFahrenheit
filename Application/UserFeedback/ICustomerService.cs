namespace Application.UserFeedback;

public interface ICustomerService<TModel> : IService<TModel> where TModel : TObject
{
    public Task<TModel> UpdateAsync(TModel userModel);

    public Task<Guid> AddAsync(TModel model);
}