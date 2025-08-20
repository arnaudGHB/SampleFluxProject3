using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using CBS.CheckManagementManagement.MediatR.Ping.Commands;
using Microsoft.AspNetCore.Authorization;

namespace CBS.CheckManagementManagement.API.Controllers
{
    [Authorize]
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
            return ReturnFormattedResponse(result);
        }
    }
}
