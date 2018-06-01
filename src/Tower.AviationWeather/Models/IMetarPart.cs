namespace Tower.AviationWeather.Models
{
    public interface IMetarPart
    {
        string RawText { get; set; }
        string ToPhonetic();
    }
}
