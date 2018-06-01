using Tower.Phonetic;

namespace Tower.AviationWeather.Models
{
    public class Visibility : IMetarPart
    {
        public string RawText { get; set; }

        public Visibility(string rawVisibilityText)
        {
            RawText = rawVisibilityText;
        }

        public string ToPhonetic()
        {
            return RawText.Contains("/") ? Distance : Distance.ToPhonetic();
        }

        public string Distance
        {
            get
            {
                // scan backwards to last digit
                var i = RawText.LastIndexOfAny("0123456789".ToCharArray());
                var vis = RawText.Substring(0, i + 1);

                if (vis == "1/2")
                    return "one half";
                if (vis == "1/4")
                    return "one quarter";

                return vis;
            }
        }
        
    }
}
