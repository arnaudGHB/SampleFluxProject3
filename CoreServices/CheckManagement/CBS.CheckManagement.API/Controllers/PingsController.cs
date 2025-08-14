using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CBS.CheckManagement.MediatR.Ping;
using CBS.CheckManagement.API.Controllers.Base;

namespace CBS.CheckManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingsController : BaseController
    {
        private readonly IMediator _mediator;

        public PingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddPing([FromBody] AddPingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
