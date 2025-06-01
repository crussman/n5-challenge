using Confluent.Kafka;

using N5.Challenge.Infrastructure.Services;

namespace N5.Challenge.Tests.Integrations;

public class KafkaServiceIntegrationTests
{
    [Fact]
    public async Task SendMessageAsync_ShouldProduceMessage()
    {
        // Arrange
        var config = new ProducerConfig { BootstrapServers = "localhost:29092" };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        var kafkaService = new KafkaService(producer);

        // Act
        var ex = await Record.ExceptionAsync(() =>
            kafkaService.SendMessageAsync(Guid.NewGuid(), "integration-test", CancellationToken.None));

        // Assert
        Assert.Null(ex);
    }
}