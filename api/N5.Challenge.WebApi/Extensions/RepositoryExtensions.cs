using N5.Challenge.Domain.Repositories;
using N5.Challenge.Infrastructure.Repositories;

namespace N5.Challenge.WebApi.Extensions;
public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryBaseAsync<>));

        var types = typeof(PermissionRepository).Assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Repository") && t.IsClass && !t.IsAbstract);

        foreach (var impl in types)
        {
            var interfaces = impl.GetInterfaces()
                .Where(i => i.Name.EndsWith("Repository"));

            foreach (var iface in interfaces)
            {
                services.AddScoped(iface, impl);
            }
        }
    }
}
