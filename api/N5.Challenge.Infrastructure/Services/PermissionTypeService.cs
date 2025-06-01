using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;
using N5.Challenge.Domain.Services;

namespace N5.Challenge.Infrastructure.Services;

public class PermissionTypeService(IPermissionTypeRepository permissionTypeRepository) : IPermissionTypeService
{
    private readonly IPermissionTypeRepository _permissionTypeRepository = permissionTypeRepository
        ?? throw new ArgumentNullException(nameof(permissionTypeRepository));

    public async Task<PermissionType?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _permissionTypeRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<PermissionType>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _permissionTypeRepository.GetAllAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return (await GetByIdAsync(id, cancellationToken)) != null;
    }
}