using N5.Challenge.Domain.Dtos;

namespace N5.Challenge.Domain.Services;

public interface IElasticsearchService
{
    Task IndexPermissionAsync(PermissionElasticSearchDto permission, CancellationToken cancellationToken);
}
