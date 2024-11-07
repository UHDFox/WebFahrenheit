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
            return Ok(_mapper.Map<PumpResponse>(await _context.Pumps.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                _mapper.Map<IReadOnlyCollection<PumpResponse>>(await _context.Pumps.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<PumpResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdatePumpRequest request)
        {
            var entity = await _context.Pumps.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("Pump to update not found");

            _mapper.Map(request, entity);

            _context.Pumps.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.Pumps.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("Pump to delete not found");

            _context.Pumps.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}