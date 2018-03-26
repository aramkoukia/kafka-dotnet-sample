using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace kafka_prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // The Kafka endpoint address
            string kafkaEndpoint = "your_kafka_endpoint";

            // The Kafka topic we'll be using
            string kafkaTopic = "testtopic";

            // Create the producer configuration
            var producerConfig = new Dictionary<string, object> { { "bootstrap.servers", kafkaEndpoint } };

            // Create the producer
            using (var producer = new Producer<Null, string>(producerConfig, null, new StringSerializer(Encoding.UTF8)))
            {
                // Send 10 messages to the topic
                for (int i = 0; i < 10; i++)
                {
                    var message = $"Event {i}";
                    var result = producer.ProduceAsync(kafkaTopic, null, message).GetAwaiter().GetResult();
                    Console.WriteLine($"Event {i} sent on Partition: {result.Partition} with Offset: {result.Offset}");
                }
            }
        }
    }
}
