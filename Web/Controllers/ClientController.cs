using AutoMapper;
using Domain;
using Domain.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Client;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        
        
        public ClientController(FahrenheitContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(CreateClientRequest client)
        {
            var result = await _context.Clients.AddAsync(mapper.Map<Client>(client));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<ClientResponse>(await _context.Clients.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }

        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<ClientResponse>(await _context.Clients.FirstAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<ClientResponse>>(await _context.Clients.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<ClientResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateClientRequest request)
        {
            var entity = await _context.Clients.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("client to update not found");

            mapper.Map(request, entity);

            _context.Clients.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.Clients.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("client to delete not found");

            _context.Clients.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
        
        
        
    }
}