using System.Text;

namespace Tower.AviationWeather.Models
{
    public class Clouds : IMetarPart
    {
        public string RawText { get; set; }

        public Clouds(string rawText)
        {
            RawText = rawText;
        }

        public string ToPhonetic()
        {
            var sb = new StringBuilder();
            
            var parts = RawText.Split(' ');
            foreach (var p in parts)
            {
                if (sb.Length > 0)
                    sb.Append(",");

                var cloudHeightIndex = p.IndexOfAny("0123456789".ToCharArray());

                var cloudText = cloudHeightIndex != -1 ? p.Substring(0, cloudHeightIndex) : p;

                var cloudHeight = 0;
                if (cloudHeightIndex != -1)
                {
                    int.TryParse(p.Substring(cloudHeightIndex), out cloudHeight);
                    cloudHeight *= 100;
                }

                if (cloudText == "FEW")
                    sb.Append($"few clouds {cloudHeight}");
                if (cloudText == "BKN")
                    sb.Append($"broken {cloudHeight}");
                if (cloudText == "SCT")
                    sb.Append($"scattered {cloudHeight}");
                if (cloudText == "OVC")
                    sb.Append($"overcast {cloudHeight}");
                if (cloudText == "SKC" || cloudText == "CLR" || cloudText == "NSC")
                    sb.Append("sky clear");
            }

            return sb.ToString();
        }

        

    }
}
