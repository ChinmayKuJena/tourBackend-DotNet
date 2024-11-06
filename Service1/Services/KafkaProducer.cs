using Confluent.Kafka;
using System;
using System.Threading.Tasks;



    public class KafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }
        // """ KAFKA PRODUCER Which produced all the response to topic  """
        public async Task ProduceMessageAsync(string message)
        {
            string topic = "request";
            try
            {
                var result = await _producer.ProduceAsync(topic, new Message<Null, string> { Value = message });
                Console.WriteLine($"Message '{message}' produced to '{result.TopicPartitionOffset}'.");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error producing message: {e.Error.Reason}");
            }
        }
    }
