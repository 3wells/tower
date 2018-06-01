using System;
using Tower.Phonetic;

namespace Tower.AviationWeather.Models
{
    public class TemperatureAndDewpoint : IMetarPart
    {
        public string RawText { get; set; }

        public TemperatureAndDewpoint(string rawText)
        {
            RawText = rawText;
        }

        public string ToPhonetic()
        {
            return
                $"temperature, {Temparature.ToPhonetic().Replace("Mike", "minus")}, dewpoint, {Dewpoint.ToPhonetic().Replace("Mike", "minus")}";
        }

        public string Temparature
            => RawText.Substring(0, RawText.IndexOf("/", StringComparison.CurrentCultureIgnoreCase));
        public string Dewpoint
            => RawText.Substring(RawText.IndexOf("/", StringComparison.CurrentCultureIgnoreCase) + 1);
    }
}
