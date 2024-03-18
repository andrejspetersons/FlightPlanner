using FlightPlanner.Extensions;
using FlightPlanner.UseCases.Cleanup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class CleanupApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CleanupApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("clear")]
        public async Task<IActionResult> ClearAsync()
        {
            return (await _mediator.Send(new CleanupCommand())).ToActionResult();
        }
    }
}
