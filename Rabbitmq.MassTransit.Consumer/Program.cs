
using MassTransit;
using Rabbitmq.MassTransit.Consumer.Consumer;
using Rabbitmq.MassTransit.Shared.Messages;

string rabbitmqUri = "amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr";
string queuename = "example-queue";
IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host(rabbitmqUri);
    factory.ReceiveEndpoint(queuename, endpoint =>
    {
        endpoint.Consumer<MessageConsumer>();
    });
});

await bus.StartAsync();
Console.Read();