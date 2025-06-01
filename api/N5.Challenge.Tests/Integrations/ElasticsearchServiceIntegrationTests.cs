using Elastic.Clients.Elasticsearch;

using N5.Challenge.Domain.Dtos;
using N5.Challenge.Infrastructure.Services;

namespace N5.Challenge.Tests.Integrations;

public class ElasticsearchServiceIntegrationTests
{
    [Fact]
    public async Task IndexPermissionAsync_ShouldIndexDocument()
    {
        // Arrange
        var settings = new ElasticsearchClientSettings(new Uri("http://localhost:19200"))
            .DefaultIndex("permissions");
        var client = new ElasticsearchClient(settings);
        var elasticService = new ElasticsearchService(client);

        var dto = new PermissionElasticSearchDto(12345, "Integraton", "Test", 1, "Read", DateTime.UtcNow);

        // Act
        var ex = await Record.ExceptionAsync(() =>
            elasticService.IndexPermissionAsync(dto, CancellationToken.None));

        // Assert
        Assert.Null(ex);
    }
}