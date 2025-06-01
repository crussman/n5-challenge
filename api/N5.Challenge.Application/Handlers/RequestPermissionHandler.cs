using AutoMapper;

using MediatR;

using N5.Challenge.Application.Commands;
using N5.Challenge.Application.Dtos;
using N5.Challenge.Domain.Services;
using N5.Challenge.Domain.ValueObjects;

namespace N5.Challenge.Application.Handlers;

public class RequestPermissionHandler(IPermissionService permissionService, IMapper mapper) :
    IRequestHandler<RequestPermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService = permissionService
        ?? throw new ArgumentNullException(nameof(permissionService));
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<PermissionDto> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
    {
        var modificationData = _mapper.Map<PermissionCreationData>(request);
        var modifiedPermission = await _permissionService.CreatePermissionAsync(modificationData, cancellationToken);

        return _mapper.Map<PermissionDto>(modifiedPermission);
    }
}