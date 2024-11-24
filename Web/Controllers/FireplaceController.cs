using Application.Fireplace;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Fireplace;
using Web.Contracts.Requests.Fireplace.Requests;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FireplaceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IFireplaceService _fireplaceService;


        public FireplaceController(IMapper mapper, IFireplaceService fireplaceService)
        {
            _mapper = mapper;
            _fireplaceService = fireplaceService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync([FromForm] CreateFireplaceRequest request, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return BadRequest("Image file is required.");
            }

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
            var result = await _fireplaceService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
            return Ok(new GetAllResponse<FireplaceResponse>(_mapper.Map<IReadOnlyCollection<FireplaceResponse>>(result),
                result.Count));
        }


        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromForm] UpdateFireplaceRequest request, IFormFile? imageFile)
        {
            var entity = await _fireplaceService.UpdateAsync(_mapper.Map<FireplaceModel>(request), imageFile);

            return Ok(new UpdatedResponse(entity.Id));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _fireplaceService.DeleteAsync(id);

            return Ok(new DeletedResponse(id, result));
        }
    }
}