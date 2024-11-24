﻿using Microsoft.AspNetCore.Http;

namespace Application.User;

public interface IUserService : ICustomerService<UserModel>
{
    public Task<string> LoginAsync(LoginModel model, HttpContext context);
    public Task<Guid> RegisterAsync(RegisterModel model);
}