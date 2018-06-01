using System.Linq;
using Tower.Phonetic;

namespace Tower.AviationWeather.Models
{
    public class Wind : IMetarPart
    {
        public string RawText { get; set; }

        public Wind(string rawWindText)
        {
            RawText = rawWindText;
        }

        public string ToPhonetic()
        {
            return $"{Direction.ToPhonetic()} at {Speed.ToPhonetic()}";
        }

        public string Direction => RawText.Length >= 3 ? RawText.Substring(0, 3) : "";

        public int Speed
        {
            get
            {
                if (RawText.Length < 4)
                    return 0;
                
                int speed;
                return int.TryParse(new string(RawText.Substring(3).Where(char.IsDigit).ToArray()), out speed)
                    ? speed
                    : 0;
            }
        }
    }
}
