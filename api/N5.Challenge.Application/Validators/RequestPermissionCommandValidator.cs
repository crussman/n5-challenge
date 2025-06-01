using FluentValidation;

using N5.Challenge.Application.Commands;

namespace N5.Challenge.Application.Validators;

public class RequestPermissionCommandValidator : AbstractValidator<RequestPermissionCommand>
{
    public RequestPermissionCommandValidator()
    {
        RuleFor(x => x.EmployeeFirstName).NotNull().NotEmpty();
        RuleFor(x => x.EmployeeLastName).NotNull().NotEmpty();
        RuleFor(x => x.PermissionTypeId).GreaterThan(0);
        RuleFor(x => x.PermissionDate).NotNull().NotEmpty();
    }
}
