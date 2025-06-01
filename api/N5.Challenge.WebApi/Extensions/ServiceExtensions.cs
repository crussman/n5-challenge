using N5.Challenge.Domain.Services;
using N5.Challenge.Infrastructure.Services;

namespace N5.Challenge.WebApi.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        var serviceInterfaceType = typeof(IElasticsearchService).Assembly;
        var assembly = typeof(ElasticsearchService).Assembly;

        var implementations = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsInterface && !t.IsAbstract);

        foreach (var implementation in implementations)
        {
            var interfaceType = implementation.GetInterfaces().FirstOrDefault(i =>
                i.Name.EndsWith("Service") && i.Assembly == serviceInterfaceType);

            if (interfaceType == null)
                continue;

            services.AddScoped(interfaceType, implementation);
        }
    }
}