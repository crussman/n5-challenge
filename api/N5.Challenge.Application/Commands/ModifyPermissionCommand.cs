using MediatR;

using N5.Challenge.Application.Dtos;

namespace N5.Challenge.Application.Commands;

public record ModifyPermissionCommand(
    int Id,
    string EmployeeFirstName,
    string EmployeeLastName,
    int PermissionTypeId,
    DateTime PermissionDate) : IRequest<PermissionDto>;
