using Confluent.Kafka;

using N5.Challenge.Domain.Services;

namespace N5.Challenge.Infrastructure.Services
{
    public class KafkaService(IProducer<Null, string> producer) : IKafkaService
    {
        private readonly IProducer<Null, string> _producer = producer ?? throw new ArgumentNullException(nameof(producer));
        private readonly string _topic = "permissions-topic";

        public async Task SendMessageAsync(Guid id, string operationName, CancellationToken cancellationToken)
        {
            var message = $"{{ \"Id\": \"{id}\", \"OperationName\": \"{operationName}\" }}";
            await _producer.ProduceAsync(_topic, new Message<Null, string> { Value = message }, cancellationToken);
        }
    }
}
