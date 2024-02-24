using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _flightlocks = new object();

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = FlightStorage.GetFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);

        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            lock (_flightlocks)
            {
                if (flight == null)
                {
                    return BadRequest(flight);
                }

                if (
                    string.IsNullOrEmpty(flight.Carrier) ||
                    string.IsNullOrEmpty(flight.ArrivalTime) ||
                    string.IsNullOrEmpty(flight.DepartureTime) ||
                    string.IsNullOrEmpty(flight.From.City) ||
                    string.IsNullOrEmpty(flight.From.Country) ||
                    string.IsNullOrEmpty(flight.From.AirportCode) ||
                    string.IsNullOrEmpty(flight.To.City) ||
                    string.IsNullOrEmpty(flight.To.Country) ||
                    string.IsNullOrEmpty(flight.To.AirportCode)
                )
                {
                    return BadRequest(flight);
                }



                if (flight.From.isEqualAirport(flight.To))
                {
                    return BadRequest("Invalid airport");
                }

                if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
                {
                    return BadRequest("Invalid date");
                }

                if (FlightStorage.FlightExist(flight))
                {
                    return Conflict("Flight Already Exist");
                }

                FlightStorage.AddFlight(flight);
                return Created($"Created flight {flight.Id}", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_flightlocks)
            {
                if (FlightStorage.DeleteFlightById(id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
        }
    }
}
