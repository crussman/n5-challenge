using Confluent.Kafka;

using Moq;

using N5.Challenge.Infrastructure.Services;

namespace N5.Challenge.Tests.Services
{
    public class KafkaServiceTests
    {
        [Fact]
        public async Task SendMessageAsync_ProducesMessage_ShouldSucceed()
        {
            // Arrange
            var producerMock = new Mock<IProducer<Null, string>>();
            producerMock
                .Setup(p => p.ProduceAsync(
                    It.IsAny<string>(),
                    It.IsAny<Message<Null, string>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new DeliveryResult<Null, string>());

            var kafkaService = new KafkaService(producerMock.Object);

            // Act
            var ex = await Record.ExceptionAsync(() =>
                kafkaService.SendMessageAsync(Guid.NewGuid(), "get", CancellationToken.None));

            // Assert
            Assert.Null(ex);
            producerMock.Verify(p => p.ProduceAsync(
                It.IsAny<string>(),
                It.IsAny<Message<Null, string>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}