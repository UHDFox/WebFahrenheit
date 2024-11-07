namespace Repository;

public interface IRepository<TEntity>
{
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(int offset, int limit);

    Task<int> GetTotalAmountAsync();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(TEntity data);

    void Update(TEntity data);

    Task<bool> DeleteAsync(Guid id);

    Task<int> SaveChangesAsync();
}