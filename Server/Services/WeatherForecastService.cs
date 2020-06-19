using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Weather;

namespace BlazorWasmDemo.Grpc.Services
{
    public class WeatherForecastService : WeatherForecast.WeatherForecastBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override Task<WeatherForecastsResponse> GetWeatherForecasts(Empty request, ServerCallContext context)
        {
            var rng = new Random();
            var weathers = Enumerable.Range(1, 5).Select(index => new Weather.Weather
            {
                Date = DateTime.UtcNow.AddDays(index).ToTimestamp(),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            var response = new WeatherForecastsResponse();
            response.Weathers.AddRange(weathers);

            return Task.FromResult(response);
        }
    }
}
