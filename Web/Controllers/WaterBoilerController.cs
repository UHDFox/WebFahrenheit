using Application.WaterBoiler;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Waterboiler;
using Web.Contracts.Requests.Waterboiler.Requests;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterBoilerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWaterBoilerService _waterBoilerService;
        
        
        public WaterBoilerController(IMapper mapper, IWaterBoilerService waterBoilerService)
        {
            _mapper = mapper;
            _waterBoilerService = waterBoilerService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateWaterBoilerRequest request, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return BadRequest("Image file is required.");
            }

            var result = await _waterBoilerService.AddAsync(_mapper.Map<WaterBoilerModel>(request), imageFile);

            var entity = await _waterBoilerService.GetByIdAsync(result);
            return Created($"{Request.Path}/{result}", _mapper.Map<WaterBoilerResponse>(entity));
        }

        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _waterBoilerService.GetByIdAsync(id);
            
            return Ok(_mapper.Map<WaterBoilerResponse>(result));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result = await _waterBoilerService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
            return Ok(new GetAllResponse<WaterBoilerResponse>(_mapper.Map<IReadOnlyCollection<WaterBoilerResponse>>(result),
                result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromForm]UpdateWaterBoilerRequest request, IFormFile? imageFile)
        {
            var entity = await _waterBoilerService.UpdateAsync(_mapper.Map<WaterBoilerModel>(request), imageFile);

            return Ok(new UpdatedResponse(entity.Id));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _waterBoilerService.DeleteAsync(id);
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}