using AutoMapper;
using Domain;
using Domain.Domain.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Feedback;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        
        
        public FeedbackController(FahrenheitContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateFeedbackRequest request)
        {
            var result = await _context.Feedbacks.AddAsync(mapper.Map<Feedback>(request));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<FeedbackResponse>(await _context.Feedbacks.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }
        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            return Ok(mapper.Map<FeedbackResponse>(await _context.Feedbacks.FirstOrDefaultAsync(e => e.Id == id)));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result =
                mapper.Map<IReadOnlyCollection<FeedbackResponse>>(await _context.Feedbacks.Skip((int)offset).Take((int)limit)
                    .ToListAsync());

            return Ok(new GetAllResponse<FeedbackResponse>(result, result.Count));
        }
        


        [HttpPut]
        public async Task<ActionResult> UpdateAsync(UpdateFeedbackRequest request)
        {
            var entity = await _context.Feedbacks.FirstOrDefaultAsync(e => e.Id == request.Id)
                         ?? throw new Exception("Feedback to update not found");

            mapper.Map(request, entity);

            _context.Feedbacks.Update(entity);

            await _context.SaveChangesAsync();

            return Ok(mapper.Map<UpdatedResponse>(entity));
        }
        
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var entity = await _context.Feedbacks.FirstOrDefaultAsync(e => e.Id == id)
                         ?? throw new Exception("Feedback to delete not found");

            _context.Feedbacks.Remove(entity);
            var result = await _context.SaveChangesAsync() > 0;
            
            return Ok(new DeletedResponse(id, result));
        }
    }
}