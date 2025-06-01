using N5.Challenge.Application.Commands;

namespace N5.Challenge.WebApi.Extensions;

public static class MediatRExtensions
{
    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ModifyPermissionCommand>());
    }
}