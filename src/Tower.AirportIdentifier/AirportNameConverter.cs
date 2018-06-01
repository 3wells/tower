using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Tower.AirportIdentifier.Models;
using CsvHelper;

namespace Tower.AirportIdentifier
{
    public interface IAirportNameConverter
    {
        Task<Models.AirportIdentifier> GetAirportIdentifierAsync(string airportName, string country);
        Task<Models.AirportIdentifier> GetAirportIdentifierByIcaoAsync(string icao);
    }

    public class AirportNameConverter : IAirportNameConverter
    {
        private readonly IAmazonS3 _s3Client;

        public AirportNameConverter(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task<Models.AirportIdentifier> GetAirportIdentifierAsync(string airportName, string country)
        {
            var response = await _s3Client.GetObjectAsync(new GetObjectRequest()
            {
                BucketName = "3wells-tower-data",
                Key = "airports.dat"
            });

            using (var textReader = new StreamReader(response.ResponseStream))
            {
                var csv = new CsvReader(textReader);
                var records = csv.GetRecords<Airport>().ToList();
                if (records == null)
                    return null;

                var airport =
                    records.FirstOrDefault(
                        a =>
                            a.Name.StartsWith(airportName, StringComparison.CurrentCultureIgnoreCase) &&
                            a.Country.Equals(string.IsNullOrEmpty(country) ? "United States" : country,
                                StringComparison.CurrentCultureIgnoreCase));

                if (airport == null)
                {
                    airport =
                        records.FirstOrDefault(
                            a => a.City.Equals(airportName, StringComparison.CurrentCultureIgnoreCase));
                }

                return airport == null ? null : new Models.AirportIdentifier(airport.Icao, airport.Name);
            }           
        }

        public async Task<Models.AirportIdentifier> GetAirportIdentifierByIcaoAsync(string icao)
        {
            if (string.IsNullOrEmpty(icao))
                return null;

            var response = await _s3Client.GetObjectAsync(new GetObjectRequest()
            {
                BucketName = "3wells-tower-data",
                Key = "airports.dat"
            });

            using (var textReader = new StreamReader(response.ResponseStream))
            {
                var csv = new CsvReader(textReader);
                var records = csv.GetRecords<Airport>().ToList();

                var airport =
                    records?.FirstOrDefault(
                        a =>
                            a.Icao.Equals(icao, StringComparison.CurrentCultureIgnoreCase));

                return airport == null ? null : new Models.AirportIdentifier(airport.Icao, airport.Name);
            }
        }
    }
}
