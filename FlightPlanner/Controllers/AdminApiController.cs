
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Models;
using AutoMapper;
using FluentValidation;
using FlightPlanner.Services;

namespace FlightPlanner.Controllers
{
    [Authorize]
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private static readonly object _flightlocks = new object();
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddFlightRequest> _validator;


        public AdminApiController(IFlightService flightService,IMapper mapper,IValidator<AddFlightRequest> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validator = validator;         
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            lock (_flightlocks)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<AddFlightResponse>(flight));
            }
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult AddFlight(AddFlightRequest request)
        {
            lock (_flightlocks)
            {
                var flight = _mapper.Map<Flight>(request);
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                if (_flightService.isEqualAirport(flight))
                {
                    return BadRequest(flight);
                }                

                if (_flightService.flightExist(flight))
                {
                    return Conflict(flight);
                }

                _flightService.Create(flight);
                
                return Created("", _mapper.Map<AddFlightResponse>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_flightlocks)
            {
                var flight = _flightService.GetFullFlightById(id);

                if (flight == null)
                {
                    return Ok();
                }
                else
                {
                    if (!_flightService.flightExist(flight))
                    {
                        return BadRequest();
                    }
                    else
                    {
                        _flightService.Delete(flight);
                        return Ok();
                    }
                }
            }
        }
    }
}
