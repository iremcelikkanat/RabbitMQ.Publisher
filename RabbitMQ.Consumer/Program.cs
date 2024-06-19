//Bağlantı oluşturma

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://ooxrvtzr:3JeknJn24izxwp4u7ufjbKF9trHxTApF@cow.rmq2.cloudamqp.com/ooxrvtzr");

//Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);//Consumerdakı kuyruk publisherdakı aynı yapılandırma ile birebir eşlemelidir.

//Queuedan mesaj okuma
EventingBasicConsumer consumer = new(channel);
//autoAck:false yaptıüımızda consume edilecek consumerdan bildirim gelemdiği sürece mesajın rabbitmq tarafında kuyruktan
//silinmesini engelliyoruz ve receive olarak elde ettiğimiz mesajın rabbitmqda başarılı şekilde işlendikten sonra rabbitmqdan 
//silinmesi gerektiği talimatını vermemiz gerekiyor
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer);
//BasicQos ile eşit dağilım sağlar
//prefetchSize consumer tarafından alınablicek en büyük mesaj boyutu byte cinsinde 0 sınırsız
//prefetchCountConsumer tarafından aynı anda işleme alonabilecek mesaj sayısı
//bu işlemlerin sadece o consumer mı yoksa tum consumerlarda mı yapılacagını belirler
channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yer.
    //e.body:Kuyruktakı mesajı getirir.
    //e.Body.Span veya e.Body.ToArray(): Kuyurktakı mesajın byte verisini geririr.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    //Multiple false ile sadece bu mesaja dair bildirimde bulundugumuzu işaretledik
    //true olsaydı birden fazla mesaja dair onay bildirisi gönderiri.bu ve bundan oncekı mesajların işlendiğini onaylardı.
    //Consumer mesajı başarıyla işlediğine dair uyarıyı BasicAck metod ile gerçekleştirir
    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: true);
};
Console.Read();