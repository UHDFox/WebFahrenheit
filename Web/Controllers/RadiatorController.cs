using Application.Radiator;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Radiator;
using Web.Contracts.Requests.Radiator.Requests;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RadiatorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRadiatorService _radiatorService;
        
        
        public RadiatorController(IMapper mapper, IRadiatorService radiatorService)
        {
            _mapper = mapper;
            _radiatorService = radiatorService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateRadiatorRequest request, IFormFile imageFile)
        {
            if (imageFile == null)
            {
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
            var result = await _radiatorService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
            return Ok(new GetAllResponse<RadiatorResponse>(_mapper.Map<IReadOnlyCollection<RadiatorResponse>>(result),
                result.Count));
        }
        

        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromForm]UpdateRadiatorRequest request, IFormFile? imageFile)
        {
            var entity = await _radiatorService.UpdateAsync(_mapper.Map<RadiatorModel>(request), imageFile);

            return Ok(new UpdatedResponse(entity.Id));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _radiatorService.DeleteAsync(id);
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}