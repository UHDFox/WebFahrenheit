using Application.Product.WaterBoiler;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Waterboiler;
using Web.Contracts.Requests.Waterboiler.Requests;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WaterBoilerController : Controller
{
    private readonly ILogger<WaterBoilerController> _logger;
    private readonly IMapper _mapper;
    private readonly IWaterBoilerService _waterBoilerService;


    public WaterBoilerController(IMapper mapper, IWaterBoilerService waterBoilerService,
        ILogger<WaterBoilerController> logger)
    {
        _mapper = mapper;
        _waterBoilerService = waterBoilerService;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateWaterBoilerRequest request, IFormFile imageFile)
    {
        var result = await _waterBoilerService.AddAsync(_mapper.Map<WaterBoilerModel>(request), imageFile);

        var entity = await _waterBoilerService.GetByIdAsync(result);
        return Created($"{Request.Path}/{result}", _mapper.Map<WaterBoilerResponse>(entity));
    }


    [HttpGet("id:guid")]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin, User")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _waterBoilerService.GetByIdAsync(id);

        return Ok(_mapper.Map<WaterBoilerResponse>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
    {
        _logger.LogInformation($"Received request for WaterBoilers: offset={offset}, limit={limit}");
        try
        {
            var result = await _waterBoilerService.GetListAsync(offset.GetValueOrDefault(), limit.GetValueOrDefault());
            _logger.LogInformation("Request processed successfully.");
            return Ok(new GetAllResponse<WaterBoilerResponse>(
                _mapper.Map<IReadOnlyCollection<WaterBoilerResponse>>(result), result.Count));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetListAsync");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    [HttpPut]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<ActionResult> UpdateAsync([FromForm] UpdateWaterBoilerRequest request, IFormFile? imageFile)
    {
        var entity = await _waterBoilerService.UpdateAsync(_mapper.Map<WaterBoilerModel>(request), imageFile);

        return Ok(new UpdatedResponse(entity.Id));
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _waterBoilerService.DeleteAsync(id);

        return Ok(new DeletedResponse(id, result));
    }
}