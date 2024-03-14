using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Storage;

namespace FlightPlanner.Controllers
{
    [Route("api/")]
    [ApiController]
   public class CustomerApiController : ControllerBase
    {
        private readonly FlightStorage _flights;
        public CustomerApiController(FlightStorage flights)
        {
            _flights = flights;           
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var result = _flights.AirportByKeywords(search);

            if (result.Length > 0)
            {
                return Ok(result);
            }

            return NoContent();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flights.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(flight);
            }
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            if (req.From == null || req.To == null || req.DepartureDate == null)
            {
                return BadRequest(req);
            }

            if (req.From == req.To)
            {
                return BadRequest(req);
            }

            var flights = _flights.FlightSearch(req);
            return Ok(flights);
        }
    }
}