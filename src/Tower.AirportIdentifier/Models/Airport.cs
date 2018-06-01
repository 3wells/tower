namespace Tower.AirportIdentifier.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Iata { get; set; }
        public string Icao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Altitude { get; set; }
        public double? Timezone { get; set; }
        public string Dst { get; set; }
        public string Tz { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
    }
}
