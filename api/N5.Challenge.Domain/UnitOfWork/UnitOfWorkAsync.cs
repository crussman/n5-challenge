namespace N5.Challenge.Domain.UnitOfWork;

public interface IUnitOfWorkAsync : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
