using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Repository.Feedback;
using Repository.User;
using SkiiResort.Application.Exceptions;

namespace Application.Feedback;

public sealed class FeedbackService : CustomerService<FeedbackModel, FeedbackRecord>, IFeedbackService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository,
        IFeedbackRepository feedbackRepository)
        : base(feedbackRepository, mapper)
    {
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
        _feedbackRepository = feedbackRepository;
    }


    public string ReturnNameFromToken()
    {
        // Retrieve the JWT token from the cookie 'some_cookie'
        var token = _httpContextAccessor.HttpContext.Request.Cookies["some-cookie"];

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

        // Proceed to store feedback, using name and userId, then return a Guid

        // Store the feedback (example: saving to database or a collection)
        //await SaveFeedbackAsync(feedbackId, message, name, userId);

        return name;
    }

    public async Task<Guid> AddUsingEmail(FeedbackModel model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email!)
                   ?? throw new NotFoundException();

        var feedback = new FeedbackRecord(user.Id, model.Message);

        var result = await _feedbackRepository.AddAsync(feedback);

        await _feedbackRepository.SaveChangesAsync();

        return result;
    }
}