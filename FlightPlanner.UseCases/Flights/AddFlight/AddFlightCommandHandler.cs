using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.AddFlight
{
    internal class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IValidator<AddFlightRequest> _validator;

        public AddFlightCommandHandler(IFlightService flightService, IMapper mapper, IValidator<AddFlightRequest> validator)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validator = validator;
        }

        public Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _mapper.Map<Flight>(request.AddFlightRequest);
            var validationResult = _validator.Validate(request.AddFlightRequest);

            if (!validationResult.IsValid)
            {
                return Task.FromResult(new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                });
            }

            if (_flightService.isEqualAirport(flight))
            {
                return Task.FromResult(new ServiceResult
                {
                    ResultObject=validationResult.Errors,
                    Status = HttpStatusCode.BadRequest
                });
            }

            if (_flightService.flightExist(flight))
            {
                return Task.FromResult(new ServiceResult
                {
                    ResultObject = validationResult.Errors,
                    Status = HttpStatusCode.Conflict
                });
            }

            _flightService.Create(flight);

            return Task.FromResult(new ServiceResult
            {
                ResultObject = _mapper.Map<AddFlightResponse>(flight),
                Status = HttpStatusCode.Created
            });
        }
    }
}
