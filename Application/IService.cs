namespace Application;

public interface IService<TModel> where TModel : TObject
{
    public Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit);

    public Task<TModel> GetByIdAsync(Guid id);

    public Task<bool> DeleteAsync(Guid id);
}