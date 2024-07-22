using KafkaConstants;
using KafkaDemoProducer;

KafkaProducer producer = new KafkaProducer(ConfigConstants.BootstrapServers);

Console.WriteLine("Producer Starting...");
Console.WriteLine("Please type a city name then enter.");

string? input;
while ((input = Console.ReadLine()) != null)
{
    await producer.ProduceMessageAsync(ConfigConstants.Topic, input, 42, 21);
}

Console.WriteLine("Producer Finished.");
