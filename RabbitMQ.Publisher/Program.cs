using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new();

//Creating Connection
factory.Uri = new("amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr");

//Activation to Connection and open channel
//Oluştuurlan channeler conneciton üzerinde işlem yapmayı sağlar
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Creation Queue
//exclusive ile bu kuyrukta birden fazla baglantı kullansınmı kullanmasın mı o configure edilir.
//Artık kuyruk oluştu
//durable:true ile kuyruk kalıcı hale gelir
channel.QueueDeclare(queue: "example-queue", exclusive: false,durable:true);

//Queue ya mesaj gönderme
//Rabbitmq kuyruga atacagı mesajalrı byte türünden kabul eder.Bu yuzden mesajalr byte dizisine çevirilir.

IBasicProperties properties =channel.CreateBasicProperties();
properties.Persistent = true;
for(int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes("Merhaba");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message,basicProperties: properties);

}




//oluşturdugum kuyruga channel üzerinde mesaj gönderirim.
//Boş bıraktıgım için defualt exchange olur default exchande direct exchanedir.
Console.Read();