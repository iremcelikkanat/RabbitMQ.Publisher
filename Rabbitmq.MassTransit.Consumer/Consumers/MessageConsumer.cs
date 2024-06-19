using MassTransit;
using MassTransit.Transports;
using Rabbitmq.MassTransit.Shared.Messages;


namespace Rabbitmq.MassTransit.Consumer.Consumer
{
    public class MessageConsumer : IConsumer<IMessages>
    {
        public Task Consume(ConsumeContext<IMessages> context)
        {
            Console.WriteLine($"Gelenn mesaj:{context.Message.Text}");
            return Task.CompletedTask;
        }
    }




}
