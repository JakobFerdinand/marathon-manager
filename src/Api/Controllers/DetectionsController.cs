using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetectionsController : ControllerBase
{
    private static readonly List<Detection> Detections = new();

    [HttpPost]
    public IActionResult Post(Detection detection)
    {
        Detections.Add(detection);
        Console.WriteLine($"Received detection: {detection}");
        return Ok();
    }

    [HttpGet]
    public IEnumerable<Detection> Get() => Detections;
}
