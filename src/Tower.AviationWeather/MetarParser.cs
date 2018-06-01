using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Tower.AviationWeather.Models;

namespace Tower.AviationWeather
{
    public interface IMetarParser
    {
        Metar Parse(string metarText);
    }

    public class MetarParser : IMetarParser
    {
        private const string StationPattern = "[A-H,K-P,R-T,U-W,Y-Z][A-Z][A-Z][A-Z]";
        private const string TimePattern = "[0-3][0-9][0-2][0-9][0-5][0-9]Z";
        private const string WindPattern = "[0-3][0-9][0-9][0-9]{2,3}KT|[0-3][0-9][0-9][0-9]{2,3}MPS";
        private const string CloudPattern = "BKN[0-9]{1,3}|OVC[0-9]{1,3}|SCT[0-9]{1,3}|FEW[0-9]{1,3}|SKC|CLR";
        private const string TemparatureAndDewpointPattern = @"M*[0-9][0-9]\/M*[0-9][0-9]";
        private const string AltimeterPattern = "A[0-9]{4}";
        private const string VisibilityPattern = @"[0-9]{1,2}SM|[0-9]\/*[0-9]SM";

        public Metar Parse(string metarText)
        {
            var station = GetFirstOccurrence(StationPattern, metarText);
            var time = GetFirstOccurrence(TimePattern, metarText);
            var wind = GetFirstOccurrence(WindPattern, metarText);
            var clouds = GetAllOccurrences(CloudPattern, metarText);
            var temperatureAndDewpoint = GetFirstOccurrence(TemparatureAndDewpointPattern, metarText);
            var altimeter = GetFirstOccurrence(AltimeterPattern, metarText);
            var visibility = GetFirstOccurrence(VisibilityPattern, metarText);

            var metar = new Metar()
            {
                RawText = metarText,
                StationId = station,
                ZuluTime = new ZuluTime(time),
                Wind = new Wind(wind),
                Visibility = new Visibility(visibility),
                TemperatureAndDewpoint = new TemperatureAndDewpoint(temperatureAndDewpoint),
                Altimeter = new Altimeter(altimeter),
                Clouds = new Clouds(string.Join(" ", clouds))
            };
             
            return metar;
        }

        public string GetFirstOccurrence(string pattern, string text)
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var m = regex.Match(text);            
            return m.Success ? m.Value : "";
        }

        public List<string> GetAllOccurrences(string pattern, string text)
        {
            List<string> valueList = null;

            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var m = regex.Match(text);

            while (m.Success)
            {
                if (valueList == null)
                    valueList = new List<string>();

                valueList.Add(m.Value);
                m = m.NextMatch();
            }

            return valueList;
        }
    }
}
