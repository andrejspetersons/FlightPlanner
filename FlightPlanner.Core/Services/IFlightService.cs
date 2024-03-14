using FlightPlanner.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService:IEntityService<Flight>
    {
        Flight? GetFullFlightById(int id);
        bool flightExist(Flight flight);
        bool isEqualAirport(Flight flight);
        PageResult SearchFlights(SearchFlightsRequest req);
    }
}
