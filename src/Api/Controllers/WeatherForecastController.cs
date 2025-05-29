using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController : ControllerBase
{
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var randomNumber = new Random();
        var temp = 0;

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = temp = randomNumber.Next(-20, 55),
            Summary = GetSummary(temp)
        }).ToArray();
    }

    private static string GetSummary(int temp)
        => temp switch
        {
            >= 32 => "Hot",
            <= 16 and > 0 => "Cold",
            <= 0 => "Freezing",
            _ => "Mild"
        };
}
