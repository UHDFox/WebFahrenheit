using Application.UserFeedback.Feedback;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Contracts.CommonResponses;
using Web.Contracts.Requests.Feedback;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : Controller
    {
        private readonly IMapper mapper;
        private readonly IFeedbackService _service;

        public FeedbackController(IMapper mapper, IFeedbackService service)
        {
            this.mapper = mapper;
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddAsync(CreateFeedbackRequest req)
        {
            var feedBack = new FeedbackModel(Guid.NewGuid(), req.Email, req.Message, req.UserId);   
            var result = await _service.AddAsync(feedBack);
            await _service.SaveChangesAsync();

            return Created($"{Request.Path}",
                mapper.Map<FeedbackResponse>(await _service.GetByIdAsync(result)));
        }

        [HttpPost("createFeedback")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Guid>> AddUserMadeAsync(CreateFeedbackRequest request)
        {
            Guid result;
            if (!string.IsNullOrEmpty(request.Email))
            {
                result = await _service.AddUsingEmail(new FeedbackModel(new Guid(), request.Email, request.Message,
                    request.UserId));
            }
            else
            {
                result = await _service.AddAsync(new FeedbackModel(new Guid(), request.Email, request.Message,
                    request.UserId));
            }

            var feedback = await _service.GetByIdAsync(result);

            return Created($"{Request.Path}", mapper.Map<FeedbackResponse>(feedback));
        }

        [HttpGet("id:guid")]
        [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            return Ok(mapper.Map<FeedbackResponse>(result));
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
        public async Task<IActionResult> GetListAsync(int? offset = 0, int? limit = 5)
        {
            var result = await _service.GetListAsync(offset.GetValueOrDefault(0), limit.GetValueOrDefault(5));

            return Ok(new GetAllResponse<FeedbackResponse>(mapper.Map<IReadOnlyCollection<FeedbackResponse>>(result),
                result.Count));
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin, HighLevelAdmin, LowLevelAdmin")]
        public async Task<ActionResult> UpdateAsync(UpdateFeedbackRequest req)
        {
            await _service.GetByIdAsync(req.Id);


            var result = await _service.UpdateAsync(new FeedbackModel(req.Id, "", req.Message, req.UserId));

            await _service.SaveChangesAsync();

            return Ok(new UpdatedResponse(result.Id));
        }

        [HttpDelete]
        [Authorize(Roles = "SuperAdmin, HighLevelAdmin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _service.DeleteAsync(id);

            return Ok(new DeletedResponse(id, result));
        }
    }
}