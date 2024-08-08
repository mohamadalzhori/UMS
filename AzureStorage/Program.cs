using System.Text;
using System.Text.Json;
using AzureStorage;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "FileQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    var uploadMessageDto = JsonSerializer.Deserialize<UploadMessageDto>(message);

    if (uploadMessageDto != null) _ = FileStorageService.UploadFileAsync(uploadMessageDto);
    
};

channel.BasicConsume(queue: "FileQueue",
    autoAck: true,
    consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();
