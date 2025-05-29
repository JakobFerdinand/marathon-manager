using System.Net;
using Shared;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api;

public class DetectionFunction(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<DetectionFunction>();

    [Function("Detections")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        var detection = await req.ReadFromJsonAsync<Detection>();
        if (detection is not null)
        {
            _logger.LogInformation("Received detection {Detection}", detection);
        }
        var response = req.CreateResponse(HttpStatusCode.Accepted);
        return response;
    }
}

