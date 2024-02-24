namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }

        public override bool Equals(object obj)
        {
            Flight other = (Flight)obj;
            return this.Carrier == other.Carrier &&
                   this.DepartureTime == other.DepartureTime &&
                   this.ArrivalTime == other.ArrivalTime&&
                   this.From.AirportCode.Trim().ToLower()==other.From.AirportCode.Trim().ToLower() &&
                   this.To.AirportCode.Trim().ToLower() == other.To.AirportCode.Trim().ToLower();
        }
    }
}
