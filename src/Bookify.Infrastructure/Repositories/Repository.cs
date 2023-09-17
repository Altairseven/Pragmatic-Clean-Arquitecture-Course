using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal abstract class Repository<TEntity, TEntityId> 
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId Id, CancellationToken ct = default)
    {
        return await _db.Set<TEntity>()
            .FirstOrDefaultAsync(x => x.Id == Id, ct);
    }

    public void Add(TEntity entity)
    {
        _db.Add(entity);
    }
}