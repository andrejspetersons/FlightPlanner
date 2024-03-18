using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {

        }

        public bool flightExist(Flight flight)
        {
            lock (SharedLock.LockObject)
            {
                var duplicates = _context.Flights.Include(f => f.To).Include(f => f.From).Where(
                    f => f.Carrier == flight.Carrier &&
                    f.DepartureTime == flight.DepartureTime &&
                    f.ArrivalTime == flight.ArrivalTime &&
                    f.From.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower() &&
                    f.To.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower()
                    ).ToList();

                return duplicates.Count > 0;
            }
        }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                    .Include(flight => flight.From)
                    .Include(flight => flight.To)
                    .SingleOrDefault(flight => flight.Id == id);

        }

        public bool isEqualAirport(Flight flight)
        {
                if (flight.To == null || flight.From == null)
                {
                    return false;
                }

                return flight.From.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower();
        }

        public PageResult SearchFlights(SearchFlightsRequest req)
        {
                var flights = _context.Flights.Where(
                    f => f.From.AirportCode == req.From &&
                    f.To.AirportCode == req.To &&
                    f.DepartureTime.Contains(req.DepartureDate)
                );

                int page = flights.Count() > 0 ? 1 : 0;
                int totalItems = flights.Count();
                List<Flight> items = flights.ToList();

                PageResult pageResult = new PageResult
                {
                    Page = page,
                    TotalItems = totalItems,
                    Items = items,
                };

                return pageResult;
            }
    }
}
