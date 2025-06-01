using AutoMapper;

using MediatR;

using N5.Challenge.Application.Commands;
using N5.Challenge.Application.Dtos;
using N5.Challenge.Domain.Services;
using N5.Challenge.Domain.ValueObjects;

namespace N5.Challenge.Application.Handlers;

public class ModifyPermissionHandler(IPermissionService permissionService, IMapper mapper) :
    IRequestHandler<ModifyPermissionCommand, PermissionDto>
{
    private readonly IPermissionService _permissionService = permissionService
        ?? throw new ArgumentNullException(nameof(permissionService));
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<PermissionDto> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
    {
        var modificationData = _mapper.Map<PermissionModificationData>(request);
        var modifiedPermission = await _permissionService.ModifyPermissionAsync(modificationData, cancellationToken);

        return _mapper.Map<PermissionDto>(modifiedPermission);
    }
}