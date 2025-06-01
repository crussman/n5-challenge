using N5.Challenge.Domain.Entities;
using N5.Challenge.Domain.Repositories;

namespace N5.Challenge.Infrastructure.Repositories;

public class PermissionTypeRepository(AppDbContext context) :
    RepositoryBaseAsync<PermissionType>(context), IPermissionTypeRepository
{
}
