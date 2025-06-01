using N5.Challenge.Domain.Entities;

namespace N5.Challenge.Domain.Services;

public interface IPermissionTypeService
{
    Task<PermissionType?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<PermissionType>> GetAllAsync(CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}