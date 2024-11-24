using Application.Infrastructure.Authentication;
using AutoMapper;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Repository.User;
using SkiiResort.Application.Exceptions;

namespace Application.User;

internal sealed class UserService : CustomerService<UserModel, UserRecord>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordProvider _passwordProvider;

    public UserService(
        IMapper mapper,
        IUserRepository repository,
        IJwtProvider jwtProvider,
        IPasswordProvider passwordProvider)
        : base(repository, mapper)
    {
        _mapper = mapper;
        _repository = repository;
        _jwtProvider = jwtProvider;
        _passwordProvider = passwordProvider;
    }

    public async Task<Guid> AddAsync(UserModel userModel)
    {
        var entity = _mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = _passwordProvider.Generate(userModel.Password);

        var result = await _repository.AddAsync(_mapper.Map<UserRecord>(entity));

        return result;
    }

    public async Task<string> LoginAsync(LoginModel model, HttpContext context)
    {
        var user = await _repository.GetByEmailAsync(model.Email)
                   ?? throw new Exception("can't login - user with stated mail not found");

        if (!_passwordProvider.Verify(model.Password, user.PasswordHash))
        {
            throw new LoginException("Password mismatch");
        }

        var token = _jwtProvider.GenerateToken(user);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(20)
        };

        context.Response.Cookies.Append("some-cookie", token, cookieOptions);

        return token;
    }

    public async Task<Guid> RegisterAsync(RegisterModel model)
    {
        return await AddAsync(_mapper.Map<UserModel>(model));
    }
}
