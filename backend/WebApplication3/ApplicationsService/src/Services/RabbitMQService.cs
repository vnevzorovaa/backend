using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;

public class RabbitMQService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Publish<T>(string queue, T message)
    {
        _channel.QueueDeclare(queue: queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        _channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: null, body: body);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}
