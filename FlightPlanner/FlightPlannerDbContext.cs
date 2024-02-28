using FlightPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner
{
    public class FlightPlannerDbContext:DbContext
    {
        public string DbPath { get; }
        public FlightPlannerDbContext(DbContextOptions<FlightPlannerDbContext> options):base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "flight-planner.db");
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
    }
}
