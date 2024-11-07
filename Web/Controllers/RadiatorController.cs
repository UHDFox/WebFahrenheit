using AutoMapper;
using Domain;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Radiator;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RadiatorController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        
        
        public RadiatorController(FahrenheitContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateRadiatorRequest request)
        {
            var result = await _context.Radiators.AddAsync(mapper.Map<Radiator>(request));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<RadiatorResponse>(await _context.Radiators.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }
        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<RadiatorResponse>(await _context.Radiators.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<RadiatorResponse>>(await _context.Radiators.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<RadiatorResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateRadiatorRequest request)
        {
            var entity = await _context.Radiators.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("Radiator to update not found");

            mapper.Map(request, entity);

            _context.Radiators.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.Radiators.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("Radiator to delete not found");

            _context.Radiators.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}