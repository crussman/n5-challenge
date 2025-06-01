using N5.Challenge.Domain.UnitOfWork;
using N5.Challenge.Infrastructure.UnitOfWork;

namespace N5.Challenge.WebApi.Extensions;

public static class UnitOfWorkExtensions
{
    public static void AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWorkAsync, UnitOfWorkAsync>();
    }
}