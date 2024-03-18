using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.FindFlight
{
    public class FindFlightCommand:IRequest<ServiceResult>
    {
        public int Id { get; set; }

        public FindFlightCommand(int id)
        {
            Id = id;
        }
    }
}
