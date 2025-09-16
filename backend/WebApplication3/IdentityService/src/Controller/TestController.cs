using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("send")]
    public IActionResult Send()
    {
        var factory = new ConnectionFactory()
        {
            HostName = "rabbitmq",  // имя контейнера RabbitMQ
            Port = 5672,
            UserName = "guest",
            Password = "guest"
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "test_queue",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        string message = "Hello from IdentityService!";
        var body = Encoding.UTF8.GetBytes(message);

        channel.BasicPublish(exchange: "",
                             routingKey: "test_queue",
                             basicProperties: null,
                             body: body);

        return Ok("Message sent: " + message);
    }
}
