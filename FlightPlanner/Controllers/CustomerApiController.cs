using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Models;
using FlightPlanner.Core.Services;
using AutoMapper;
using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerApiController : ControllerBase
    {
        private static readonly object _flightlocker = new object();
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        private readonly IValidator<SearchFlightsRequest> _searchValidator;
        public CustomerApiController(IFlightService flightService,IAirportService airportService,IMapper mapper,IValidator<SearchFlightsRequest> searchValidator)
        {
            _flightService = flightService;
            _airportService = airportService;
            _mapper = mapper;
            _searchValidator = searchValidator;
        }

        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirport(string search)
        {
            var airport = _airportService.AirportsByKeywords(search);
            return Ok(_mapper.Map<IEnumerable<AirportViewModel>>(airport)); ///what!!
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFullFlightById(id);
            return flight == null ? NotFound() : Ok(_mapper.Map<AddFlightResponse>(flight));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
            var validationResult = _searchValidator.Validate(req);
            if (!validationResult.IsValid)
            {
                return BadRequest(req);
            }

            var response = _flightService.SearchFlights(req);
            return Ok(response);
        }
    }
}