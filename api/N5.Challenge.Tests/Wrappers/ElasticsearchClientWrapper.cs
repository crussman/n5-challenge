using Elastic.Clients.Elasticsearch;

using N5.Challenge.Domain.Dtos;

namespace N5.Challenge.Tests.Wrappers;

public interface IElasticsearchClientWrapper
{
    Task<bool> IndexExistsAsync(string indexName, CancellationToken cancellationToken = default);
    Task<bool> IndexPermissionAsync(PermissionElasticSearchDto permission, CancellationToken cancellationToken = default);
}

public class ElasticsearchClientWrapper : IElasticsearchClientWrapper
{
    private readonly ElasticsearchClient _client;

    public ElasticsearchClientWrapper(ElasticsearchClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<bool> IndexExistsAsync(string indexName, CancellationToken cancellationToken = default)
    {
        var response = await _client.Indices.ExistsAsync(indexName, cancellationToken);
        return response.Exists;
    }

    public async Task<bool> IndexPermissionAsync(PermissionElasticSearchDto permission, CancellationToken cancellationToken = default)
    {
        var response = await _client.IndexAsync(permission, idx => idx.Index("permissions"), cancellationToken);
        return response.IsValidResponse;
    }
}
