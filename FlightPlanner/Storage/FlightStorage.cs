using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;

        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public void AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public Flight? GetFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public bool FlightExist(Flight flight)
        {
            var duplicates = _context.Flights.Where(f => f.Carrier == flight.Carrier &&
                f.DepartureTime == flight.DepartureTime &&
                f.ArrivalTime == flight.ArrivalTime &&
                f.From.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower() &&
                f.To.AirportCode.Trim().ToLower() == flight.To.AirportCode.Trim().ToLower()).ToList();

            return duplicates.Count > 0;   
        }

        public bool DeleteFlightById(int id)
        {
            var flightToRemove = _context.Flights.Find(id);

            if (flightToRemove == null)
            {
                return true;
            }
            else
            {
                if (!_context.Flights.Contains(flightToRemove))
                {
                    return false;
                }

                _context.Remove(flightToRemove);
                _context.SaveChanges();
                return true;
            }
        }

        public Airport[] AirportByKeywords(string keywords)
        {
            keywords = keywords.Trim();
            return _context.Airports
                .Where(a => a.Country.ToLower().Contains(keywords.ToLower())||
                a.City.ToLower().Contains(keywords.ToLower()) ||
                a.AirportCode.ToLower().Contains(keywords.ToLower()))
                .ToArray();
        }

        public PageResult<Flight> FlightSearch(SearchFlightsRequest req)
        {
            var validFlights = _context.Flights.Where(f => f.From.AirportCode == req.From &&
                f.To.AirportCode == req.To &&
                f.DepartureTime.Contains(req.DepartureDate));

            int page = validFlights.Count() > 0 ? 1 : 0;
            int totalItems = validFlights.Count();
            List<Flight> items = validFlights.ToList();

            PageResult<Flight> result = new PageResult<Flight>
            {
                Page = page,
                TotalItems = totalItems,
                Items = items
            };

            return result;
        }

        public void Clear()
        {
            _context.RemoveRange(_context.Flights);
            _context.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }
    }
}
