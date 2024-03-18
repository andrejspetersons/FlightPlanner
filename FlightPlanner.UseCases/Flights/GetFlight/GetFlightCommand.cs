using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.GetFlight
{
    public class GetFlightCommand:IRequest<ServiceResult>
    {
        public int Id { get; set; }

        public GetFlightCommand(int id)
        {
            Id = id;       
        }
    }
}
