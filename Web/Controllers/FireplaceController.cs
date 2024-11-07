using AutoMapper;
using Domain;
using Domain.Domain.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Fireplace;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FireplaceController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        
        
        public FireplaceController(FahrenheitContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateFireplaceRequest request)
        {
            var result = await _context.Fireplaces.AddAsync(mapper.Map<Fireplace>(request));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<FireplaceResponse>(await _context.Fireplaces.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }
        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<FireplaceResponse>(await _context.Fireplaces.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<FireplaceResponse>>(await _context.Fireplaces.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<FireplaceResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateFireplaceRequest request)
        {
            var entity = await _context.Fireplaces.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("Fireplace to update not found");

            mapper.Map(request, entity);

            _context.Fireplaces.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.Fireplaces.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("Fireplace to delete not found");

            _context.Fireplaces.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}