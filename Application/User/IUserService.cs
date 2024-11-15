﻿using Microsoft.AspNetCore.Http;

namespace Application.User;

public interface IUserService
{
    Task<IReadOnlyCollection<GetUserModel>> GetAllAsync(int offset, int limit);

    Task<GetUserModel> GetByIdAsync(Guid id);

    Task<Guid> AddAsync(AddUserModel userModel);

    Task<bool> DeleteAsync(Guid id);

    Task<UpdateUserModel> UpdateAsync(UpdateUserModel userModel);

    Task<string> LoginAsync(LoginModel model, HttpContext context);

    Task<Guid> RegisterAsync(RegisterModel model);
}