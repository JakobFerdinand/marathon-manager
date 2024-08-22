using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Text.Json;

namespace MarathonManager.Services;

public interface IQueueService
{
    Task SendMessageAsync<T>(T serviceBusMessage, string queueName);
}

public sealed class QueueService(IConfiguration config) : IQueueService
{
    public async Task SendMessageAsync<T>(T serviceBusMessage, string queueName)
    {
        QueueClient queueClient = new(config.GetConnectionString("AzureServiceBus"), queueName);
        string messageBody = JsonSerializer.Serialize(serviceBusMessage);
        Message message = new(Encoding.UTF8.GetBytes(messageBody));
        await queueClient.SendAsync(message);
    }
}