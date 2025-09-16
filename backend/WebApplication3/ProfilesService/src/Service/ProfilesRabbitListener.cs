using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ProfilesService.Service
{
    public class ProfilesRabbitListener
    {
        public void Start()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "test_queue",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] ProfilesService received: {0}", message);
            };

            channel.BasicConsume(
                queue: "test_queue",
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" [*] ProfilesService is listening for messages...");
            Console.ReadLine(); // чтобы процесс не завершился сразу
        }
    }
}

