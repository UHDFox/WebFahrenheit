using Application.Infrastructure.Exceptions;
using AutoMapper;
using Domain.Domain.Entities;
using Microsoft.Extensions.Logging;
using Repository;

namespace Application.UserFeedback;

public abstract class CustomerService<TModel, TRecord> : ICustomerService<TModel>
    where TModel : CustomerItem 
    where TRecord : IUserFeedback
{
    private readonly IMapper _mapper;
    private readonly IRepository<TRecord> _repository;
    private readonly ILogger<CustomerService<TModel, TRecord>> _logger;

    protected CustomerService(IRepository<TRecord> repository, IMapper mapper, ILogger<CustomerService<TModel, TRecord>> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<TModel>> GetListAsync(int offset, int limit)
    {
        return _mapper.Map<IReadOnlyCollection<TModel>>(await _repository.GetAllAsync(offset, limit));
    }

    public async Task<TModel> GetByIdAsync(Guid id)
    {
        var record = await _repository.GetByIdAsync(id);
        if (record == null)
        {
            _logger.LogError($"Record with id: {id} not found in {typeof(TModel).Name}");
            throw new NotFoundException($"Record not found in {typeof(TModel).Name}");
        }
        
        _logger.LogInformation($"Retrieved record with id: {id} in {typeof(TModel).Name}");
        
        return _mapper.Map<TModel>(record);
    }

    public async Task<Guid> AddAsync(TModel userModel)
    {
        var entity = _mapper.Map<TModel>(userModel);

        var result = await _repository.AddAsync(_mapper.Map<TRecord>(entity));

        await _repository.SaveChangesAsync();
        
        _logger.LogInformation($"Created record with id: {result} in {typeof(TModel).Name}");
        
        return result;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            await GetByIdAsync(id);

            var result = await _repository.DeleteAsync(id);

            await _repository.SaveChangesAsync();

            _logger.LogInformation(result
                ? $"Deleted record with Id: {id} in {typeof(TModel).Name}"
                : $"Could not delete record with id: {id} in {typeof(TModel).Name}");

            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError($"An error occurred while deleting record with Id: {id}, Message: {ex.Message}");
            return false;
        }
    }

    public async Task<TModel> UpdateAsync(TModel userModel)
    {
        try
        {
            var entity = await _repository.GetByIdAsync(userModel.Id);

            if (entity == null)
            {
                _logger.LogError($"Record with id: {userModel.Id} not found in {typeof(TModel).Name} while updating");
                throw new NotFoundException($"Record with entity{userModel.Id} not found in {typeof(TModel).Name}");
            }

            _mapper.Map(userModel, entity);

            _repository.Update(entity);

            await _repository.SaveChangesAsync();

            _logger.LogInformation($"Successfully updated record with id: {userModel.Id} in {typeof(TModel).Name}");

            return userModel;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while updating entity with id: {userModel.Id}, \n Message: {ex.Message}");
            throw;
        }
    }
}