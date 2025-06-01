using Elastic.Clients.Elasticsearch;

using N5.Challenge.Domain.Dtos;
using N5.Challenge.Domain.Services;

namespace N5.Challenge.Infrastructure.Services;

public class ElasticsearchService(ElasticsearchClient client) : IElasticsearchService
{
    private readonly ElasticsearchClient _client = client ?? throw new ArgumentNullException(nameof(client));

    public async Task IndexPermissionAsync(PermissionElasticSearchDto permission, CancellationToken cancellationToken)
    {
        if (!(await _client.Indices.ExistsAsync("permissions", cancellationToken)).Exists)
            await _client.Indices.CreateAsync("permissions", cancellationToken);

        var response = await _client.IndexAsync(permission, idx => idx.Index("permissions"), cancellationToken);

        if (!response.IsValidResponse)
            throw new Exception($"Failed to index document: {response.DebugInformation}");
    }
}
