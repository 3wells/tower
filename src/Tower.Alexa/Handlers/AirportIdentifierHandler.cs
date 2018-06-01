using System.Threading.Tasks;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;
using Tower.AirportIdentifier;

namespace Tower.Alexa.Handlers
{
    public interface IAirportIdentifierHandler
    {
        Task<SkillResponse> GetAirportIdentifierAsync(SkillRequest input);
    }

    public class AirportIdentifierHandler : IAirportIdentifierHandler
    {
        private readonly IAirportNameConverter _airportNameConverter;
        private readonly ISlotManager _slotManager;

        public AirportIdentifierHandler(IAirportNameConverter airportNameConverter, ISlotManager slotManager)
        {
            _airportNameConverter = airportNameConverter;
            _slotManager = slotManager;
        }

        public async Task<SkillResponse> GetAirportIdentifierAsync(SkillRequest input)
        {
            var airportName = _slotManager.GetSlotValue(input.Request.Intent.Slots, "AirportName");
            var airport = string.IsNullOrEmpty(airportName)
                ? null
                : await _airportNameConverter.GetAirportIdentifierAsync(airportName, null);

            return new SkillResponse
            {
                Response = new Response
                {
                    ShouldEndSession = true,
                    OutputSpeech = new PlainTextOutputSpeech
                    {
                        Text = airport == null
                            ? $"I was unable to find an airport identifier for {(string.IsNullOrEmpty(airportName) ? "the given airport" : airportName)}"
                            : $"The airport identifier for {airportName} is {airport.Letters.Replace(" ", ",")}, that's {airport.Phonetic.Replace(" ", ",")}"
                    }
                },
                Version = "1.0"
            };
        }   
    }
}
