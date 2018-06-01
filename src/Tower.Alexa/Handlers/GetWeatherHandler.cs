using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;
using Tower.AirportIdentifier;
using Tower.AviationWeather;
using Tower.AviationWeather.Models;
using Tower.Phonetic;

namespace Tower.Alexa.Handlers
{
    public interface IGetWWeatherHandler
    {
        Task<SkillResponse> GetWeatherAsync(SkillRequest input);
    }

    public class GetWeatherHandler : IGetWWeatherHandler
    {
        private readonly IAviationWeatherRetriever _aviationWeatherRetriever;
        private readonly IAirportNameConverter _airportNameConverter;
        private readonly ISlotManager _slotManager;

        private const string HelpText = "You can ask Control Tower, what is the weather at Santa Barbara, or, " +
                                        "what is the weather at Kilo Sierra Bravo Alpha. For what airport " +
                                        "would you like weather?";

        public GetWeatherHandler(IAviationWeatherRetriever aviationWeatherRetriever,
            IAirportNameConverter airportNameConverter, ISlotManager slotManager)
        {
            _aviationWeatherRetriever = aviationWeatherRetriever;
            _airportNameConverter = airportNameConverter;
            _slotManager = slotManager;
        }

        public async Task<SkillResponse> GetWeatherAsync(SkillRequest input)
        {
            try
            {
                string icao;
                AirportIdentifier.Models.AirportIdentifier airportIdentifier;

                var airportName = _slotManager.GetSlotValue(input.Request.Intent.Slots, "AirportName");
                if (airportName.Equals("help", StringComparison.CurrentCultureIgnoreCase))
                    return GetSkillResponse(HelpText, false);
                if (airportName.Equals("exit", StringComparison.CurrentCultureIgnoreCase) ||
                    airportName.Equals("cancel", StringComparison.CurrentCultureIgnoreCase) ||
                    airportName.Equals("stop", StringComparison.CurrentCultureIgnoreCase))
                    return GetSkillResponse("", true);

                if (!string.IsNullOrEmpty(airportName))
                {
                    airportIdentifier = await _airportNameConverter.GetAirportIdentifierAsync(airportName, null);
                    icao = airportIdentifier != null ? airportIdentifier.Icao : "";
                }
                else
                {
                    icao = GetIcaoFromSlots(input.Request.Intent.Slots);
                    airportIdentifier = await _airportNameConverter.GetAirportIdentifierByIcaoAsync(icao);
                }

                var metar = string.IsNullOrEmpty(icao) ? null : await _aviationWeatherRetriever.GetMetarAsync(icao);

                return GetSkillResponse(metar != null
                    ? TranslateMetarToText(metar, airportIdentifier)
                    : "I was unable to find a weather report for " +
                      (string.IsNullOrEmpty(icao) ? "the given airport." : icao.ToPhonetic()), true);
            }
            catch (Exception e)
            {
                LambdaLogger.Log(e.StackTrace);
                throw;
            }
        }        

        private string GetIcaoFromSlots(Dictionary<string, Slot> slots)
        {
            var icao = "";

            if (_slotManager.SlotHasValue(slots, "First") &&
                _slotManager.SlotHasValue(slots, "Second") &&
                _slotManager.SlotHasValue(slots, "Third"))
            {
                if (!_slotManager.SlotHasValue(slots, "Fourth"))
                    icao = "K";
                icao += slots["First"].Value.FromPhonetic();
                icao += slots["Second"].Value.FromPhonetic();
                icao += slots["Third"].Value.FromPhonetic();
                icao += slots.ContainsKey("Fourth") &&
                        !string.IsNullOrEmpty(slots["Fourth"].Value)
                    ? slots["Fourth"].Value.FromPhonetic()
                    : "";
            }

            return icao;
        }
       
        private SkillResponse GetSkillResponse(string responseText, bool shoulEndSession)
        {
            return new SkillResponse
            {
                Response = new Response
                {
                    ShouldEndSession = shoulEndSession,
                    OutputSpeech = new PlainTextOutputSpeech {Text = responseText}
                },
                Version = "1.0"
            };
        }

        private static string TranslateMetarToText(Metar metar, AirportIdentifier.Models.AirportIdentifier airportIdentifier)
        {
            return
                $"Here are the current weather conditions at {airportIdentifier.Name}, {airportIdentifier.Icao.ToPhonetic().Replace(" ", ",")}, " +
                $"As of, {metar.ZuluTime.ToPhonetic()}, " +
                $"wind, {metar.Wind.ToPhonetic()}, " +
                $"visibility, {metar.Visibility.ToPhonetic()}, " +
                $"{metar.Clouds.ToPhonetic()}, " +
                $"{metar.TemperatureAndDewpoint.ToPhonetic()}, " +
                $"altimeter, {metar.Altimeter.ToPhonetic()}";
        }
    }
}
