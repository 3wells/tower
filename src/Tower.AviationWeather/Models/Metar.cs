namespace Tower.AviationWeather.Models
{
    public class Metar
    {
        public string RawText { get; set; }
        public string StationId { get; set; }
        public ZuluTime ZuluTime { get; set; }
        public Wind Wind { get; set; }
        public Visibility Visibility { get; set; }
        public TemperatureAndDewpoint TemperatureAndDewpoint { get; set; }
        public Altimeter Altimeter { get; set; }
        public Clouds Clouds { get; set; }
    }
}
