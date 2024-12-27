using Application.Product.Radiator;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Radiator;
using Web.Contracts.Requests.Radiator.Requests;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadiatorController : Controller
{
    private readonly ILogger<RadiatorController> _logger;
    private readonly IMapper _mapper;
    private readonly IRadiatorService _radiatorService;


    public RadiatorController(IMapper mapper, IRadiatorService radiatorService, ILogger<RadiatorController> logger)
    {
        _mapper = mapper;
        _radiatorService = radiatorService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateRadiatorRequest request, IFormFile imageFile)
    {
        if (imageFile == null)
        {
            _logger.LogError("imageFile isn't provided while creating radiator");
            return BadRequest("Image file is required.");
        }

        var result = await _radiatorService.AddAsync(_mapper.Map<RadiatorModel>(request), imageFile);

        var entity = await _radiatorService.GetByIdAsync(result);
        return Created($"{Request.Path}/{result}", _mapper.Map<RadiatorResponse>(entity));
    }


    [HttpGet("id:guid")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _radiatorService.GetByIdAsync(id);

        return Ok(_mapper.Map<RadiatorResponse>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
    {
        _logger.LogInformation($"Received request for radiators: offset={offset}, limit={limit}");
        try
        {
            var result = await _radiatorService.GetListAsync(offset.GetValueOrDefault(), limit.GetValueOrDefault());
            _logger.LogInformation("Request processed successfully.");
            return Ok(new GetAllResponse<RadiatorResponse>(_mapper.Map<IReadOnlyCollection<RadiatorResponse>>(result),
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
    public async Task<ActionResult> UpdateAsync([FromForm] UpdateRadiatorRequest request, IFormFile? imageFile)
    {
        var entity = await _radiatorService.UpdateAsync(_mapper.Map<RadiatorModel>(request), imageFile);

        return Ok(new UpdatedResponse(entity.Id));
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _radiatorService.DeleteAsync(id);

        return Ok(new DeletedResponse(id, result));
    }
}