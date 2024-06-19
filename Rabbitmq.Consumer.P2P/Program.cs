using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region P2P(Point to Point) Tasarımı
string queueName = "example-p2p-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: false,
    consumer: consumer);
consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
#endregion
