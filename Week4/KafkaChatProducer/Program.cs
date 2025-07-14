using Confluent.Kafka;

Console.WriteLine("Kafka Chat Producer Started. Type messages to send:");

var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

using var producer = new ProducerBuilder<Null, string>(config).Build();

while (true)
{
    var message = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(message)) break;

    var result = await producer.ProduceAsync("chat-topic", new Message<Null, string> { Value = message });
    Console.WriteLine($"Sent: {message} to partition {result.Partition} at offset {result.Offset}");
}
