using MediatR;

using N5.Challenge.Application.Dtos;

namespace N5.Challenge.Application.Commands;

public record RequestPermissionCommand(
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    DateTime PermissionDate) : IRequest<PermissionDto>;
