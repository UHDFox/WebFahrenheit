using Application.Product.Fireplace;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Fireplace;
using Web.Contracts.Requests.Fireplace.Requests;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FireplaceController : Controller
{
    private readonly IFireplaceService _fireplaceService;
    private readonly ILogger<FireplaceController> _logger;
    private readonly IMapper _mapper;


    public FireplaceController(IMapper mapper, IFireplaceService fireplaceService, ILogger<FireplaceController> logger)
    {
        _mapper = mapper;
        _fireplaceService = fireplaceService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateFireplaceRequest request, IFormFile imageFile)
    {
        var result = await _fireplaceService.AddAsync(_mapper.Map<FireplaceModel>(request), imageFile);

        var entity = await _fireplaceService.GetByIdAsync(result);
        return Created($"{Request.Path}/{result}", _mapper.Map<FireplaceResponse>(entity));
    }


    [HttpGet("id:guid")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _fireplaceService.GetByIdAsync(id);

        return Ok(_mapper.Map<FireplaceResponse>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
    {
        _logger.LogInformation($"Received request for fireplaces: offset={offset}, limit={limit}");
        try
        {
            var result = await _fireplaceService.GetListAsync(offset.GetValueOrDefault(), limit.GetValueOrDefault());
            _logger.LogInformation("Request processed successfully.");
            return Ok(new GetAllResponse<FireplaceResponse>(_mapper.Map<IReadOnlyCollection<FireplaceResponse>>(result),
                result.Count));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetListAsync");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }


    [HttpPut]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<ActionResult> UpdateAsync([FromForm] UpdateFireplaceRequest request, IFormFile? imageFile)
    {
        var entity = await _fireplaceService.UpdateAsync(_mapper.Map<FireplaceModel>(request), imageFile);

        return Ok(new UpdatedResponse(entity.Id));
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _fireplaceService.DeleteAsync(id);

        return Ok(new DeletedResponse(id, result));
    }
}