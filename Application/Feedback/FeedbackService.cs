using System.IdentityModel.Tokens.Jwt;
using Application.Feedback.Models;
using AutoMapper;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Http;
using Repository.FeedbackRepository;
using Repository.User;
using SkiiResort.Application.Exceptions;

namespace Application.Feedback;

public sealed class FeedbackService : IFeedbackService
{
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;
    private readonly IFeedbackRepository _feedbackRepository;

    public FeedbackService(IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IFeedbackRepository feedbackRepository)
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
        Guid feedbackId = Guid.NewGuid();  // Generate feedback ID

        // Store the feedback (example: saving to database or a collection)
        //await SaveFeedbackAsync(feedbackId, message, name, userId);

        return name;
    }

    public async Task<Guid> AddUsingEmail(AddFeedbackModel model)
    {
        var user = await _userRepository.GetByEmailAsync(model.Email!)
                   ?? throw new NotFoundException();

        var feedback = new FeedbackRecord(model.Message, user.Id);

        var result = await _feedbackRepository.AddAsync(feedback);

        await _feedbackRepository.SaveChangesAsync();

        return result;
    }
    
    public async Task<IReadOnlyCollection<GetFeedbackModel>> GetListAsync(int offset, int limit)
    {
        var totalAmount = await _feedbackRepository.GetTotalAmountAsync();

        return _mapper.Map<IReadOnlyCollection<GetFeedbackModel>>(await _feedbackRepository.GetAllAsync(offset, limit));
    }

    public async Task<GetFeedbackModel> GetByIdAsync(Guid id)
    {
        var result = await _feedbackRepository.GetByIdAsync(id) ?? throw new Exception();
        return _mapper.Map<GetFeedbackModel>(result);
    }

    public async Task<Guid> AddAsync(AddFeedbackModel model)
    {
        var feedback = _mapper.Map<FeedbackRecord>(model);

        var result = await _feedbackRepository.AddAsync(feedback);

        await _feedbackRepository.SaveChangesAsync();
        
        return result;
    }

    public async Task<UpdateFeedbackModel> UpdateAsync(UpdateFeedbackModel model, IFormFile? imageFile)
    {
        var entity = await _feedbackRepository.GetByIdAsync(model.Id)
                     ?? throw new Exception("Feedback entity not found");
        
        _mapper.Map(model, entity);

        _feedbackRepository.Update(entity);
        
        await _feedbackRepository.SaveChangesAsync();

        return model;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await _feedbackRepository.GetByIdAsync(id);
        if (entity == null)
            throw new Exception("Entity not found");

        var result = await _feedbackRepository.DeleteAsync(id);

        await _feedbackRepository.SaveChangesAsync();

        return result;
    }
}