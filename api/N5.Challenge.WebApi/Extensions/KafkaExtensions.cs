using Confluent.Kafka;

namespace N5.Challenge.WebApi.Extensions;

public static class KafkaExtensions
{
    public static void AddKafka(this IServiceCollection services)
    {
        services.AddSingleton(sp => new ProducerBuilder<Null, string>(new ProducerConfig
        {
            BootstrapServers = "kafka:19092"
        }).Build());
    }
}