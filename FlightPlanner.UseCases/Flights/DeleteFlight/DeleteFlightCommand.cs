using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.DeleteFlight
{
    public class DeleteFlightCommand:IRequest<ServiceResult>
    {
        public int Id { get; set; }

        public DeleteFlightCommand(int id)
        {
            Id = id;    
        }
    }
}
