using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tower.AviationWeather.Models;

namespace Tower.AviationWeather
{
    public interface IAviationWeatherRetriever
    {
        Task<Metar> GetMetarAsync(string icao);

    }

    public class AviationWeatherRetriever : IAviationWeatherRetriever
    {
        private readonly IMetarParser _metarParser;

        private const string BaseUrl =
            "https://aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=3&mostRecent=true&stationString=";

        public AviationWeatherRetriever(IMetarParser metarParser)
        {
            _metarParser = metarParser;
        }

        public async Task<Metar> GetMetarAsync(string icao)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{BaseUrl}{icao}");

                var xmlStream = await response.Content.ReadAsStreamAsync();

                var doc = XDocument.Load(xmlStream);

                try
                {
                    var metarText = doc.Descendants().FirstOrDefault(element => element.Name == "raw_text").Value;
                    return _metarParser.Parse(metarText);
                }
                catch
                {
                    return null;
                }                
            }
        }
    }
}
