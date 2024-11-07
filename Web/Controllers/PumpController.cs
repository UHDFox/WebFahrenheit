using Application.Infrastructure.Images;
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
        private readonly IMapper mapper;
        private readonly IImageService _imageService;
        
        
        public PumpController(FahrenheitContext context, IMapper mapper, IImageService imageService)
        {
            _context = context;
            this.mapper = mapper;
            _imageService = imageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync([FromForm] CreatePumpRequest request, IFormFile imageFile)
        {
            // Validate request data
            if (imageFile == null)
            {
                return BadRequest("Image file is required.");
            }
            
            var pump = mapper.Map<Pump>(request);
            
            pump.ImagePath = await _imageService.SaveImageLocallyAsync(imageFile, "pumps");

            // Add and save the new pump entity to the database
            var result = await _context.Pumps.AddAsync(pump);
            await _context.SaveChangesAsync();

            // Return the created Pump with its image path
            return Created($"{Request.Path}/{result.Entity.Id}", mapper.Map<PumpResponse>(result.Entity));
        }

        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<PumpResponse>(await _context.Pumps.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<PumpResponse>>(await _context.Pumps.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<PumpResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdatePumpRequest request)
        {
            var entity = await _context.Pumps.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("Pump to update not found");

            mapper.Map(request, entity);

            _context.Pumps.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
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