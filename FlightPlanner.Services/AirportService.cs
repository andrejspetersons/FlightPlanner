using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        public AirportService(IFlightPlannerDbContext context) : base(context)
        {

        }

        public Airport[] AirportsByKeywords(string keyword)
        {
            keyword = keyword.Trim();
            return _context.Airports.Where(
                a =>
                a.Country.ToLower().Contains(keyword.ToLower()) ||
                a.City.ToLower().Contains(keyword.ToLower()) ||
                a.AirportCode.ToLower().Contains(keyword.ToLower()))
                .ToArray();
        }
    }
}
