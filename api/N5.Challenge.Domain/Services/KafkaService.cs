namespace N5.Challenge.Domain.Services;

public interface IKafkaService
{
    Task SendMessageAsync(Guid id, string operationName, CancellationToken cancellation);
}
