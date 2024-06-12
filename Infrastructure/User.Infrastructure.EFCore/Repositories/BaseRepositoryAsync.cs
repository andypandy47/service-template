using System.Linq.Expressions;
using User.Domain.Entities;
using User.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace User.Infrastructure.EFCore.Repositories;

public class BaseRepositoryAsync<T>(ApplicationDbContext context) : IBaseRepositoryAsync<T> where T : BaseEntity
{
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<IList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public IQueryable<T> GetAll()
    {
        return context.Set<T>().AsNoTracking();
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().FirstOrDefaultAsync(expression);
    }

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        context.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().CountAsync(expression);
    }
}