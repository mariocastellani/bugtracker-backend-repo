using Application.Issues;

namespace ApiService.Controllers.Issues
{
    [ApiController]
    [Route("[controller]")]
    public class IssuesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IssuesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<Result<List<IssueDto>>> Get()
        {
            return await _mediator.Send(new GetAllIssuesRequest());
        }

        [HttpGet("AssignedToMe")]
        public async Task<Result<List<IssueDto>>> GetAssignedToMe()
        {
            var request = new GetAssignedToMeRequest()
            {
                MyUserName = User.Identity.Name ?? ""
            };

            return await _mediator.Send(request);
        }

        [HttpPost]
        public async Task<Result<IssueDto>> Post([FromBody] CreateIssueRequest request)
        {
            return await _mediator.Send(request);
        }
    }
}