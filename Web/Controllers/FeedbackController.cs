using Application.Feedback;
using AutoMapper;
using Domain;
using Domain.Domain.Entities.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Feedback;
using Web.Contracts.Requests.User;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly FahrenheitContext _context;
        private readonly IMapper mapper;
        private readonly IFeedbackService _service;


        
        
        public FeedbackController(FahrenheitContext context, IMapper mapper, IFeedbackService service)
        {
            _context = context;
            this.mapper = mapper;
            this._service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateFeedbackRequest request)
        {
            var result = await _context.Feedbacks.AddAsync(mapper.Map<FeedbackRecord>(request));
            await _context.SaveChangesAsync();

            return Created($"{Request.Path}", 
                mapper.Map<FeedbackResponse>(await _context.Feedbacks.FirstOrDefaultAsync(e => e.Id == result.Entity.Id)));
        }
        
        [HttpPost("createFeedback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddUserMadeAsync(CreateFeedbackRequest request)
        {
            Guid result;
            if (request.Email is null)
            {
                result = await _service.AddUsingEmail(mapper.Map<AddFeedbackModel>(request));
            }
            else
            {
                result = await _service.AddAsync(mapper.Map<AddFeedbackModel>(request));
            }
            var feedback = await _service.GetByIdAsync(result);
            
            return Created($"{Request.Path}", mapper.Map<FeedbackResponse>(feedback));
        }
        
        [HttpGet("id:guid")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            
            return Ok(mapper.Map<FeedbackResponse>(result));
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