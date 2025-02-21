using System.Collections.ObjectModel;
using Application.Infrastructure.Authentication;
using Application.Infrastructure.Exceptions;
using AutoMapper;
using Contracts.Contracts.User;
using Domain.Domain.Entities.Users;
using FahrenheitAuthService.Client;
using Microsoft.Extensions.Logging;

namespace Application.UserFeedback.User;

internal sealed class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordProvider _passwordProvider;
    private ILogger<UserService> _logger;
    private IAuthClient _authClient;
    
    public UserService(
        IMapper mapper,
        IJwtProvider jwtProvider,
        IPasswordProvider passwordProvider,
        ILogger<UserService> logger,
            IAuthClient client)
    {
        _mapper = mapper;
        _jwtProvider = jwtProvider;
        _passwordProvider = passwordProvider;
        _logger = logger;
        _authClient = client;
    }

    public async Task<Guid> AddAsync(UserModel userModel)
    {
        var entity = _mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = _passwordProvider.Generate(userModel.Password);

        var result = await _authClient.AddAsync(_mapper.Map<CreateUserRequest>(entity));
        
        _logger.LogInformation($"Created new user with id {result}");

        return result.Id;
    }

     public async Task<IReadOnlyCollection<UserModel>> GetListAsync(int offset, int limit)
     {
         var result = await _authClient.GetListAsync(offset, limit);

        return result.List.Select(_mapper.Map<UserModel>).ToList();
    }

    public async Task<UserModel> GetByIdAsync(Guid id)
    {
        var record = await _authClient.GetByIdAsync(id);
        if (record == null)
        {
            _logger.LogError($"Record with id: {id} not found in {typeof(UserModel).Name}");
            throw new NotFoundException($"Record not found in {typeof(UserModel).Name}");
        }
        
        _logger.LogInformation($"Retrieved record with id: {id} in {typeof(UserModel).Name}");
        
        return _mapper.Map<UserModel>(record);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            await GetByIdAsync(id);

            var result = await _authClient.DeleteAsync(id);

            _logger.LogInformation(result
                ? $"Deleted record with Id: {id} in {typeof(UserModel).Name}"
                : $"Could not delete record with id: {id} in {typeof(UserModel).Name}");

            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError($"An error occurred while deleting record with Id: {id}, Message: {ex.Message}");
            return false;
        }
    }

    public async Task<UserModel> UpdateAsync(UserModel userModel)
    {
        try
        {
            var entity = await _authClient.GetByIdAsync(userModel.Id);

            if (entity == null)
            {
                _logger.LogError($"Record with id: {userModel.Id} not found in {typeof(UserModel).Name} while updating");
                throw new NotFoundException($"Record with entity{userModel.Id} not found in {typeof(UserModel).Name}");
            }

            _mapper.Map(userModel, entity);

            await _authClient.UpdateAsync(_mapper.Map<UpdateUserRequest>(entity));

            _logger.LogInformation($"Successfully updated record with id: {userModel.Id} in {typeof(UserModel).Name}");

            return userModel;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occured while updating entity with id: {userModel.Id}, \n Message: {ex.Message}");
            throw;
        }
    }
}