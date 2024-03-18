using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Models;
using MediatR;
using FlightPlanner.UseCases.Airports;
using FlightPlanner.Extensions;
using FlightPlanner.UseCases.Flights.SearchFlight;
using FlightPlanner.UseCases.Flights.FindFlight;

namespace FlightPlanner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomerApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("airports")]
        public async Task<IActionResult> SearchAirport(string search)
        {
            return (await _mediator.Send(new SearchKeywordCommand(search))).ToActionResult();
        }

        [HttpGet]
        [Route("flights/{id}")]
        public async Task<IActionResult> FindFlightById(int id)
        {
            return (await _mediator.Send(new FindFlightCommand(id))).ToActionResult();
        }

        [HttpPost]
        [Route("flights/search")]
        public async Task<IActionResult> SearchFlights(SearchFlightsRequest req)
        {
            return (await _mediator.Send(new SearchFlightCommand(req))).ToActionResult();
        }
    }
}