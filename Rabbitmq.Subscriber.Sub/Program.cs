using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region Publish/Subscribe(Pub/Sub) Pattern
string exchangeName = "pub-sub-exchange";
string queueName = channel.QueueDeclare().QueueName;


channel.ExchangeDeclare(
    exchange: exchangeName,
    type: ExchangeType.Fanout);

channel.QueueBind(
    queue: queueName,
    exchange: exchangeName,
    routingKey: string.Empty);
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
Console.ReadKey();