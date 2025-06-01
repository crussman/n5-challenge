using FluentValidation;

using N5.Challenge.Application.Validators;

namespace N5.Challenge.WebApi.Extensions;

public static class FluentValidationExtensions
{
    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ModifyPermissionCommandValidator>();
    }
}