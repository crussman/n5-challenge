using Microsoft.EntityFrameworkCore;

using N5.Challenge.Domain.Repositories;

using System.Linq.Expressions;

namespace N5.Challenge.Infrastructure.Repositories;

public class RepositoryBaseAsync<TEntity>(AppDbContext context) : IRepositoryAsync<TEntity> where TEntity : class
{
    protected readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<TEntity?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id, cancellationToken);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
        CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToArrayAsync(cancellationToken);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Update(TEntity entity) => _dbSet.Update(entity);
}