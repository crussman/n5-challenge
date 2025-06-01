using AutoMapper;

using MediatR;

using N5.Challenge.Application.Dtos;
using N5.Challenge.Application.Queries;
using N5.Challenge.Domain.Services;

namespace N5.Challenge.Application.Handlers;

public class GetPermissionsHandler(IPermissionService permissionService, IMapper mapper) :
    IRequestHandler<GetPermissionsQuery, IEnumerable<PermissionDto>>
{
    private readonly IPermissionService _permissionService = permissionService
        ?? throw new ArgumentNullException(nameof(permissionService));
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<IEnumerable<PermissionDto>> Handle(GetPermissionsQuery _, CancellationToken cancellationToken)
    {
        var permissions = await _permissionService.GetPermissionsAsync(cancellationToken);

        return _mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }
}