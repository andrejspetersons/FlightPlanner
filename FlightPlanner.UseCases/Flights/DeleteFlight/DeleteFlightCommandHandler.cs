using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, ServiceResult>
    {
        private IFlightService _flightService;

        public DeleteFlightCommandHandler(IFlightService flightService)
        {
            _flightService = flightService;
        }
        public Task<ServiceResult> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetFullFlightById(request.Id);

            if (flight == null)
            {
                return Task.FromResult(new ServiceResult());
            }
            else
            {
                if (!_flightService.flightExist(flight))
                {
                    return Task.FromResult(new ServiceResult
                    {
                        Status = HttpStatusCode.BadRequest
                    });
                }
                else
                {
                    _flightService.Delete(flight);
                    return Task.FromResult(new ServiceResult());
                }
            }
        }
    }
}
