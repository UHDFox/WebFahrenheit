using Application.UserFeedback;
using Application.UserFeedback.User;
using AutoMapper;
using Domain.Domain.Entities.Users;
using FahrenheitAuthService.Client.Implemetations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.User;
using LoginRequest = Web.Contracts.Requests.User.LoginRequest;
using RegisterRequest = Web.Contracts.Requests.User.RegisterRequest;

namespace Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public sealed class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IMapper mapper;
    private readonly IUserService userService;
    

    public UserController(IUserService userService, IMapper mapper, ILogger<UserController> logger)
    {
        this.userService = userService;
        this.mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAllResponse<UserRecord>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetListAsync(int? offset, int? limit)
    {
        _logger.LogInformation($"Received request for users: offset={offset}, limit={limit}");
        try
        {
            var result = await userService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
            _logger.LogInformation("Request processed successfully.");
            return Ok(new GetAllResponse<UserResponse>(mapper.Map<IReadOnlyCollection<UserResponse>>(result),
                result.Count));
        }
        catch (Exception ex)
        {
            _logger.LogError("Error in GetListAsync: {Message}", ex.Message);
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpGet("id:guid")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserRecord))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        return Ok(mapper.Map<UserResponse>(await userService.GetByIdAsync(id)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddAsync(CreateUserRequest data)
    {
        var result = await userService.AddAsync(mapper.Map<UserModel>(data));
        return Created($"{Request.Path}", mapper.Map<UserResponse>(await userService.GetByIdAsync(result)));
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdatedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync(UpdateUserRequest data)
    {
        await userService.UpdateAsync(mapper.Map<UserModel>(data));
        return Ok(new UpdatedResponse(data.Id));
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeletedResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await userService.DeleteAsync(id);
        return Ok(new DeletedResponse(id, result));
    }
}