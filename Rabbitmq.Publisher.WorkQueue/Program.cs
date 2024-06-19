using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region Work Queue Pattern
string queueName = "example-work-queue";

channel.QueueDeclare(
    queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

for (int i = 0; i < 10; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("hello" + i);

    channel.BasicPublish(
    exchange: string.Empty,
    routingKey: queueName,
    body: message);
}


#endregion

Console.Read();