using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Responses;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Tower.Alexa.Handlers
{
    public interface IIntentRequestHandler : IRequestHandler { }

    public class IntentRequestHandler : IIntentRequestHandler
    {
        private readonly IAirportIdentifierHandler _airportIdentifierHandler;
        private readonly IGetWWeatherHandler _getWeatherHandler;
        private readonly Dictionary<string, Func<SkillRequest, Task<SkillResponse>>> _intentRequestStrategy;

        public IntentRequestHandler(IAirportIdentifierHandler airportIdentifierHandler, IGetWWeatherHandler getWeatherHandler)
        {
            _airportIdentifierHandler = airportIdentifierHandler;
            _getWeatherHandler = getWeatherHandler;
            _intentRequestStrategy = new Dictionary<string, Func<SkillRequest, Task<SkillResponse>>>()
            {
                {"GetAirportIdentifier", _airportIdentifierHandler.GetAirportIdentifierAsync},
                {"GetWeather", _getWeatherHandler.GetWeatherAsync}
            };
        }

        public Task<SkillResponse> HandleAsync(SkillRequest input)
        {
            if (!_intentRequestStrategy.ContainsKey(input.Request.Intent.Name))
                throw new Exception("Unknown intent name");

            return _intentRequestStrategy[input.Request.Intent.Name](input);
        }
    }
}
