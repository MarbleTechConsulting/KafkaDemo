using Confluent.Kafka;
using KafkaRecords;
using Newtonsoft.Json;

namespace KafkaDemoProducer
{
    public class KafkaProducer
    {
        public readonly ProducerConfig _config;
        public readonly IProducer<Null, string> _producer;

        public KafkaProducer(string config)
        {
            _config = new ProducerConfig { BootstrapServers = config };
            _producer = new ProducerBuilder<Null, string>(_config).Build();
        }

        public async Task ProduceMessageAsync(string topic, string name, int amount, int price)
        {
            var tradeInfo = new TradeInfoBuilder()
                .WithTradeName(name)
                .WithTradeAmount(amount)
                .WithTradePrice(price)
                .Build();
            await ProduceMessageAsync(topic, tradeInfo);
        }

        public async Task ProduceMessageAsync(string topic, TradeInfo tradeInfo)
        {
            try
            {
                string serializedObj = JsonConvert.SerializeObject(tradeInfo);
                var messageToProduce = new Message<Null, string> { Value = serializedObj };
                var response = await _producer.ProduceAsync(topic, messageToProduce);
                Console.WriteLine(response.Value);
            }
            catch (ProduceException<Null, string> ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
