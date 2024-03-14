using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Airport
    {
        [JsonIgnore]
        public int Id { get; set; }
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

    }
}
