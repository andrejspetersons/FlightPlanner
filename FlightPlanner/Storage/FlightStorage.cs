using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _id = 1;

        public static void AddFlight(Flight flight)
        {
            flight.Id = _id++;
            _flights.Add(flight);            
        }

        public static Flight GetFlightById(int id)
        {
            return _flights.FirstOrDefault(f => f.Id == id);
        }

        public static bool FlightExist(Flight flight)
        {
            var result = _flights.Any(f => f.Equals(flight));
            return result;
        }

        public static bool DeleteFlightById(int id)
        {
                var flightToRemove = GetFlightById(id);
                if (flightToRemove == null)
                {
                    return true;
                }
                else
                {
                    if (!_flights.Contains(flightToRemove))
                    {
                        return false;
                    }

                    _flights.Remove(flightToRemove);
                    return true;
                }
        }

        public static Airport[] AirportByKeywords(string keywords)
        {
            var result = new List<Airport>();
            keywords = keywords.Trim();

            for (int i = 0; i < _flights.Count(); i++)
            {
                if (_flights[i].From.AirportCode.Contains(keywords, StringComparison.OrdinalIgnoreCase) ||
                    _flights[i].From.Country.Contains(keywords, StringComparison.OrdinalIgnoreCase) ||
                    _flights[i].From.City.Contains(keywords, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    result.Add(_flights[i].From);
                }
            }
            
            return result.ToArray();
        }

        public static PageResult<Flight> FlightSearch(SearchFlightsRequest req)
        {
            var validFlights = _flights.Where(f => f.From.AirportCode == req.From &&
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

        public static void Clear()
        {
            _flights.Clear();
        }
    }
}
