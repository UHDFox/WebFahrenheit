using Application.Product.Pump;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Pump;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PumpController : Controller
{
    private readonly ILogger<PumpController> _logger;
    private readonly IMapper _mapper;
    private readonly IPumpService _pumpService;


    public PumpController(IMapper mapper, IPumpService pumpService, ILogger<PumpController> logger)
    {
        _mapper = mapper;
        _pumpService = pumpService;
        _logger = logger;
    }

    [HttpPost]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Guid>> AddAsync([FromForm] CreatePumpRequest request, IFormFile imageFile)
    {
        var result = await _pumpService.AddAsync(_mapper.Map<PumpModel>(request), imageFile);

        var entity = await _pumpService.GetByIdAsync(result);
        return Created($"{Request.Path}/{result}", _mapper.Map<PumpResponse>(entity));
    }


    [HttpGet("id:guid")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _pumpService.GetByIdAsync(id);

        return Ok(_mapper.Map<PumpResponse>(result));
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
    {
        _logger.LogInformation($"Received request for pumps: offset={offset}, limit={limit}");
        try
        {
            var result = await _pumpService.GetListAsync(offset.GetValueOrDefault(), limit.GetValueOrDefault());
            _logger.LogInformation("Request processed successfully.");
            return Ok(new GetAllResponse<PumpResponse>(_mapper.Map<IReadOnlyCollection<PumpResponse>>(result),
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
    public async Task<ActionResult> UpdateAsync([FromForm] UpdatePumpRequest request, IFormFile? imageFile)
    {
        var entity = await _pumpService.UpdateAsync(_mapper.Map<PumpModel>(request), imageFile);

        return Ok(new UpdatedResponse(entity.Id));
    }

    [HttpDelete]
    [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _pumpService.DeleteAsync(id);

        return Ok(new DeletedResponse(id, result));
    }
}