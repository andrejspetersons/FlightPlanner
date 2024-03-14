using FlightPlanner.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class CleanupApiController : ControllerBase
    {
        private readonly FlightStorage _flights;
        public CleanupApiController(FlightStorage flights)
        {
            _flights = flights;       
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _flights.Clear();
            return Ok();
        }

    }
}
