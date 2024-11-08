using Application.Infrastructure.Images;
using Application.Pump;
using Application.Pump.Models;
using AutoMapper;
using Domain;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Pump;
using Web.Infrastructure;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PumpController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IPumpService _pumpService;
        
        
        public PumpController(FahrenheitContext context, IMapper mapper, IImageService imageService, IPumpService pumpService)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _pumpService = pumpService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync([FromForm] CreatePumpRequest request, IFormFile imageFile)
        {
            if (imageFile == null)
            {
                return BadRequest("Image file is required.");
            }

            var result = await _pumpService.AddAsync(_mapper.Map<AddPumpModel>(request), imageFile);

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
            var result = await _pumpService.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));
            return Ok(new GetAllResponse<PumpResponse>(_mapper.Map<IReadOnlyCollection<PumpResponse>>(result),
                result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync([FromForm]UpdatePumpRequest request, IFormFile? imageFile)
        {
            var entity = await _pumpService.UpdateAsync(_mapper.Map<UpdatePumpModel>(request), imageFile);

            return Ok(new UpdatedResponse(entity.Id));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _pumpService.DeleteAsync(id);
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}