using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Slight.Alexa.Framework.Models.Requests;
using Slight.Alexa.Framework.Models.Requests.RequestTypes;
using Tower.AirportIdentifier;
using Tower.AviationWeather;
using Xunit;

namespace Tower.Tests
{
    public class FunctionTest
    {
        [Fact]
        public async Task TestToUpperFunction()
        {
            var airportNameConverter = new AirportNameConverter(new AmazonS3Client(RegionEndpoint.USEast1));
            var airport = await airportNameConverter.GetAirportIdentifierAsync("burbank", null);

            var message = $"The airport identifier is {airport.Letters} {airport.Phonetic}";
            Console.WriteLine(message);
        }

        [Fact]
        public async Task GetAirportIdentifierByIcao()
        {
            var airportNameConverter = new AirportNameConverter(new AmazonS3Client(RegionEndpoint.USEast1));
            var airport = await airportNameConverter.GetAirportIdentifierByIcaoAsync("KPDX");
        }

        [Fact]
        public async Task GetMetar()
        {
            var aviationWeatherRetriever = new AviationWeatherRetriever(new MetarParser());
            var metar = await aviationWeatherRetriever.GetMetarAsync("KVNY");

            //Console.WriteLine(metar.Wind.Speed);
        }
    }
}
