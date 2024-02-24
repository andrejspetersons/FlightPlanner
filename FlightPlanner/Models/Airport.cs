using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {
        public string Country { get; set; }
        public string City { get; set; }
        [JsonPropertyName("airport")]
        public string AirportCode { get; set; }

        public bool isEqualAirport(Airport airport)
        {
            if (airport == null)
            {
                return false;
            }

            return this.AirportCode.Trim().ToLower() == airport.AirportCode.Trim().ToLower();
        }

        /*public override int GetHashCode()
        {
            return HashCode.Combine(Country, City, AirportCode);  
        }*/

    }
}
