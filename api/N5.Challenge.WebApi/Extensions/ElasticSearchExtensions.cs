using Elastic.Clients.Elasticsearch;

namespace N5.Challenge.WebApi.Extensions;

public static class ElasticSearchExtensions
{
    public static void AddElasticSearch(this IServiceCollection services)
    {
        services.AddSingleton(sp =>
            new ElasticsearchClient(
                new ElasticsearchClientSettings(
                    new Uri("http://elasticsearch:9200"))
                .DefaultIndex("permissions")));
    }
}
