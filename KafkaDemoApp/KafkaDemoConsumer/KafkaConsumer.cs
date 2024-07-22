using Confluent.Kafka;
using KafkaConstants;
using KafkaRecords;
using Newtonsoft.Json;

namespace KafkaDemoConsumer
{
    public class KafkaConsumer
    {
        private readonly ConsumerConfig _config;
        private readonly IConsumer<Null, string> _consumer;
        public KafkaConsumer()
        {
            _config = new ConsumerConfig
            {
                GroupId = ConfigConstants.GroupId,
                BootstrapServers = ConfigConstants.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(_config).Build();

            _consumer.Subscribe(ConfigConstants.Topic);

            CancellationToken cancellationToken = new();

            try
            {
                while (true)
                {
                    var response = _consumer.Consume(cancellationToken);
                    if (response.Message != null)
                    {
                        var tradeInfo = JsonConvert.DeserializeObject<TradeInfo>(response.Message.Value);
                        if (tradeInfo != null)
                        {
                            Console.WriteLine(value: $"Trade Name: {tradeInfo.TradeName}, Amount: {tradeInfo.TradeAmount}, Price: {tradeInfo.TradePrice}");
                        }
                        else
                        {
                            Console.WriteLine("TradeInfo received was empty after deserialization.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}