namespace Application;

public interface ICustomerService<TModel> : IService<TModel> where TModel : TObject
{
    public new Task<TModel> UpdateAsync(TModel userModel);

    public new Task<Guid> AddAsync(TModel model);
}