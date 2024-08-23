namespace Shared;

public sealed class Runner
{
    public required string Firstname { get; set; }
    public required string Lastname { get; set; }
    public required int YearOfBirth { get; set; }
}

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}