using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.ValueObjects;

namespace N5.Challenge.Domain.Services
{
    public interface IPermissionService
    {
        Task<Permission> CreatePermissionAsync(PermissionCreationData permissionCreationData, CancellationToken cancellationToken);
        Task<IEnumerable<Permission>> GetPermissionsAsync(CancellationToken cancellationToken);
        Task<Permission> ModifyPermissionAsync(PermissionModificationData permissionModificationData, CancellationToken cancellationToken);
    }
}
