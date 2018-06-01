using System;
using Amazon;
using Amazon.S3;
using Microsoft.Extensions.DependencyInjection;
using Tower.AirportIdentifier;
using Tower.Alexa.Handlers;
using Tower.AviationWeather;

namespace Tower.Alexa
{
    public class ServiceProviderBuilder
    {
        public IServiceProvider Build()
        {
            return new ServiceCollection()
                .AddSingleton<IIntentRequestHandler, IntentRequestHandler>()
                .AddSingleton<ILaunchRequestHandler, LaunchRequestHandler>()
                .AddSingleton<ISessionEndedRequestHandler, SessionEndedRequestHandler>()
                .AddSingleton<IAirportIdentifierHandler, AirportIdentifierHandler>()
                .AddSingleton<IAirportNameConverter, AirportNameConverter>()
                .AddSingleton<IAmazonS3, AmazonS3Client>(a => new AmazonS3Client(RegionEndpoint.USEast1))
                .AddSingleton<IMetarParser, MetarParser>()
                .AddSingleton<IAviationWeatherRetriever, AviationWeatherRetriever>()
                .AddSingleton<IGetWWeatherHandler, GetWeatherHandler>()
                .AddSingleton<ISlotManager, SlotManager>()
                .BuildServiceProvider();
        }
    }
}
