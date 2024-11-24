using AutoMapper;
using Domain.Domain.Entities;
using Repository;
using SkiiResort.Application.Exceptions;

namespace Application;

public abstract class CustomerService<TModel, TRecord> : ICustomerService<TModel> where TModel: CustomerItem where TRecord: IUserFeedback
{
    private readonly IMapper _mapper;
    private readonly IRepository<TRecord> _repository;
    public CustomerService(IRepository<TRecord> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await _repository.GetTotalAmountAsync();

        return _mapper.Map<IReadOnlyCollection<TModel>>(await _repository.GetAllAsync(offset, limit));
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var result = await _repository.GetByIdAsync(id) ?? throw new Exception();
        return _mapper.Map<TModel>(result);
    }

    public async Task<Guid> AddAsync(TModel userModel)
    {
        var entity = _mapper.Map<TModel>(userModel);

        var result = await _repository.AddAsync(_mapper.Map<TRecord>(entity));

        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        return await _repository.DeleteAsync(id);
    }

    public async Task<TModel> UpdateAsync(TModel userModel)
    {
        var entity = await _repository.GetByIdAsync(userModel.Id)
                     ?? throw new NotFoundException("user entity not found");

        _mapper.Map(userModel, entity);
        
       _repository.Update(entity);
        await _repository.SaveChangesAsync();

        return userModel;
    }
    
    public async Task<int> SaveChangesAsync() => await _repository.SaveChangesAsync();
}