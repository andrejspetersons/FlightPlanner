using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [ApiController]
    [Route("testing-api")]
    public class CleanupApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        public CleanupApiController(IFlightService flightService,IAirportService airportService)
        {
            _flightService = flightService;
            _airportService = airportService;
        }

        [HttpPost]
        [Route("clear")]
        public IActionResult Clear()
        {
            _airportService.DeleteAll();
            _flightService.DeleteAll();
            return Ok();
        }
    }
}
