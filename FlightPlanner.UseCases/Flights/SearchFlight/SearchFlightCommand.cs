using FlightPlanner.Core.Models;
using FlightPlanner.UseCases.Models;
using MediatR;

namespace FlightPlanner.UseCases.Flights.SearchFlight
{
    public class SearchFlightCommand:IRequest<ServiceResult>
    {
       public SearchFlightsRequest SearchFlightRequest { get; set; }

        public SearchFlightCommand(SearchFlightsRequest searchFlightRequest)
        {
            SearchFlightRequest = searchFlightRequest;    
        }
    }
}
