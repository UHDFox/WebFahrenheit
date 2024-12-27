using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Domain;
using Domain.Domain.Entities.Users;
using Domain.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Feedback;
using Repository.User;

namespace Application.UserFeedback.Feedback;

public sealed class FeedbackService : CustomerService<FeedbackModel, FeedbackRecord>, IFeedbackService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IFeedbackRepository _feedbackRepository;
    public readonly FahrenheitContext _context;

    public FeedbackService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository,
        IFeedbackRepository feedbackRepository, ILogger<FeedbackService> logger, FahrenheitContext context)
        : base(feedbackRepository, mapper, logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _feedbackRepository = feedbackRepository;
        _context = context;
    }


    public string ReturnNameFromToken()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new UnauthorizedAccessException("HttpContext is not available.");
        }
        
        // Retrieve the JWT token from the cookie 'some_cookie'
        var token = _httpContextAccessor.HttpContext!.Request.Cookies["some-cookie"] ?? string.Empty;

        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("Token not found in cookies.");
        }

        // Decode the JWT token and get the payload
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        // Extract 'name' from the token's claims
        var nameClaim = jwtToken?.Claims
            .FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");

        if (nameClaim == null)
        {
            throw new UnauthorizedAccessException("Name claim not found in the token.");
        }

        var name = nameClaim.Value;

        return name;
    }

    public async Task<Guid> AddUsingEmail(FeedbackModel model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email!)
                   ?? new UserRecord("", "", "", "", UserRole.User);
        user.Id = _context.Users.First().Id;

        var feedback = new FeedbackRecord(user.Id, model.Message);

        var result = await _feedbackRepository.AddAsync(feedback);

        await _feedbackRepository.SaveChangesAsync();

        return result;
    }
}