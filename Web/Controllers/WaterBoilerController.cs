using AutoMapper;
using Domain;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Waterboiler;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WaterBoilerController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        
        
        public WaterBoilerController(FahrenheitContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateWaterBoilerRequest request)
        {
            var result = await _context.WaterBoilers.AddAsync(mapper.Map<WaterBoiler>(request));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<WaterBoilerResponse>(await _context.WaterBoilers.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }
        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<WaterBoilerResponse>(await _context.WaterBoilers.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<WaterBoilerResponse>>(await _context.WaterBoilers.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<WaterBoilerResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateWaterBoilerRequest request)
        {
            var entity = await _context.WaterBoilers.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("WaterBoiler to update not found");

            mapper.Map(request, entity);

            _context.WaterBoilers.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.WaterBoilers.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("WaterBoiler to delete not found");

            _context.WaterBoilers.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}