using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Models;
using MediatR;
using System.Net;


namespace FlightPlanner.UseCases.Flights.GetFlight
{
    public class GetFlightCommandHandler:IRequestHandler<GetFlightCommand,ServiceResult>
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        public GetFlightCommandHandler(IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
        }

        public Task<ServiceResult> Handle(GetFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = _flightService.GetFullFlightById(request.Id);

            if (flight==null)
            {
                return Task.FromResult(new ServiceResult
                {
                    Status = HttpStatusCode.NotFound
                });
            }

           
            return Task.FromResult(new ServiceResult
            {
                ResultObject = _mapper.Map<AddFlightResponse>(flight),
                Status = HttpStatusCode.OK
            });
        }
    }
}
