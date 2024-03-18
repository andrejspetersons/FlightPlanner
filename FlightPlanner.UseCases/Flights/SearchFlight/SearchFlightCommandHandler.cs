using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using FluentValidation;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.SearchFlight
{
    public class SearchFlightCommandHandler : IRequestHandler<SearchFlightCommand, ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IValidator<SearchFlightsRequest> _validator;

        public SearchFlightCommandHandler(IFlightService flightService, IValidator<SearchFlightsRequest> validator)
        {
            _flightService = flightService;
            _validator = validator;
        }
        public Task<ServiceResult> Handle(SearchFlightCommand request, CancellationToken cancellationToken)
        {
            var validateResult = _validator.Validate(request.SearchFlightRequest);

            if (!validateResult.IsValid)
            {
                return Task.FromResult(new ServiceResult
                {
                    ResultObject = validateResult.Errors,
                    Status = HttpStatusCode.BadRequest
                });
            }

            var response = _flightService.SearchFlights(request.SearchFlightRequest);
            return Task.FromResult(new ServiceResult
            {
                ResultObject = response,
                Status = HttpStatusCode.OK
            });

        }
    }
}
