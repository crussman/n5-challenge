using MediatR;

using N5.Challenge.Application.Validators;

namespace N5.Challenge.WebApi.Extensions;

public static class StartupExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddAutoMapper();
        services.AddFluentValidation();
        services.AddMediatR();
        services.AddElasticSearch();
        services.AddKafka();
        services.AddRepositories();
        services.AddUnitOfWork();
        services.AddServices();

        // Add pipeline validation behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }
}
