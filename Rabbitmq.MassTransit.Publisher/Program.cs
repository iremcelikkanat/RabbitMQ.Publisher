
using MassTransit;
using Rabbitmq.MassTransit.Shared.Messages;

string rabbitmqUri = "amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr";

string queuename = "example-queue";

IBusControl bus=Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitmqUri);
});

var sendEndpoint= await bus.GetSendEndpoint(new($"{rabbitmqUri}/{queuename}"));

Console.WriteLine("Gönderilecek Mesaj :");
string message=Console.ReadLine();
await sendEndpoint.Send<IMessages>(new Message()
{
    Text = message
});