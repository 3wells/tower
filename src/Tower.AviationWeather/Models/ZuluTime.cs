using Tower.Phonetic;

namespace Tower.AviationWeather.Models
{
    public class ZuluTime : IMetarPart
    {
        public string RawText { get; set; }

        public ZuluTime(string rawText)
        {
            RawText = rawText;
        }

        public string ToPhonetic()
        {
            return Time.ToPhonetic();
        }

        public string Date => RawText.Substring(0, 2);
        public string Time => RawText.Substring(2);
    }
}
