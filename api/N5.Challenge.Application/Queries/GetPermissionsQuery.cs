using MediatR;

using N5.Challenge.Application.Dtos;

namespace N5.Challenge.Application.Queries;

public record GetPermissionsQuery() : IRequest<IEnumerable<PermissionDto>>;
