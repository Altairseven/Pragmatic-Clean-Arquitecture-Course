using Bookify.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Repositories;

internal abstract class Repository<T> where T : Entity
{
    protected readonly ApplicationDbContext _db;

    public Repository(ApplicationDbContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<T?> GetByIdAsync(Guid Id, CancellationToken ct = default)
    {
        return await _db.Set<T>()
            .FirstOrDefaultAsync(x => x.Id == Id, ct);
    }

    public void Add(T entity)
    {
        _db.Add(entity);
    }
}