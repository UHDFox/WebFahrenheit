using Application.Infrastructure.Authentication;
using Application.Infrastructure.Exceptions;
using AutoMapper;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.User;

namespace Application.UserFeedback.User;

internal sealed class UserService : CustomerService<UserModel, UserRecord>, IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordProvider _passwordProvider;
    private ILogger<UserService> _logger;

    public UserService(
        IMapper mapper,
        IUserRepository repository,
        IJwtProvider jwtProvider,
        IPasswordProvider passwordProvider,
        ILogger<UserService> logger)
        : base(repository, mapper, logger)
    {
        _mapper = mapper;
        _repository = repository;
        _jwtProvider = jwtProvider;
        _passwordProvider = passwordProvider;
        _logger = logger;
    }

    public new async Task<Guid> AddAsync(UserModel userModel)
    {
        var entity = _mapper.Map<UserRecord>(userModel);

        entity.PasswordHash = _passwordProvider.Generate(userModel.Password);

        var result = await _repository.AddAsync(_mapper.Map<UserRecord>(entity));
        
        _logger.LogInformation($"Created new user with id {result}");

        return result;
    }

    public async Task<string> LoginAsync(LoginModel model, HttpContext context)
    {
        var user = await _repository.GetByEmailAsync(model.Email);
        
        if (user == null)
        {
            _logger.LogError($"User with email {model.Email} does not exist");
            throw new LoginException("can't login - user with stated mail not found");
        }

        if (!_passwordProvider.Verify(model.Password, user.PasswordHash))
        {
            _logger.LogError($"User with email {model.Email} does not match password: {model.Password}");
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
        var result = await AddAsync(_mapper.Map<UserModel>(model));
        
        _logger.LogInformation($"Registered new user with id {result}");

        return result;
    }
}