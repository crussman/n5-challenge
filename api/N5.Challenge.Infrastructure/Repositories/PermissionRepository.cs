using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;

using System.Linq.Expressions;

namespace N5.Challenge.Infrastructure.Repositories;

public class PermissionRepository(AppDbContext context) :
    RepositoryBaseAsync<Permission>(context), IPermissionRepository
{
    public override async Task<IEnumerable<Permission>> GetAllAsync(
        CancellationToken cancellationToken,
        params Expression<Func<Permission, object>>[]? includes)
    {
        return await base.GetAllAsync(cancellationToken, p => p.PermissionType);
    }
}
