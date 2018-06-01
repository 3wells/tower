using Tower.Phonetic;

namespace Tower.AviationWeather.Models
{
    public class Altimeter : IMetarPart
    {
        public string RawText { get; set; }

        public Altimeter(string rawText)
        {
            RawText = rawText;
        }

        public string ToPhonetic()
        {
            return Setting.ToPhonetic();
        }

        public string Setting => RawText.Substring(1);
    }
}
