using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using UMS.API.AzureStorage.Dto;
using UMS.API.Controllers.v1;

namespace UMS.API.RabbitMQ;

public class RabbitMqProducer : IRabbitMqProducer
{
    public void PublishFile(UploadMessageDto uploadMessageDto)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "FileQueue",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var message = JsonSerializer.Serialize(uploadMessageDto);
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: string.Empty,
            routingKey: "FileQueue",
            basicProperties: null,
            body: body);

        Console.WriteLine($" Message Sent");
    }
}

public class UploadMessageDto
{
    public string ContainerName { get; set; }
    public string BlobName { get; set; }
    public string FileData { get; set; }
    public string ContentType { get; set; }
}