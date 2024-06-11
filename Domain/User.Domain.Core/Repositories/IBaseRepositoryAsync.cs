using System.Linq.Expressions;
using User.Domain.Core.Entities;

namespace User.Domain.Core.Repositories;

public interface IBaseRepositoryAsync<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IList<T>> ListAllAsync();
    IQueryable<T> GetAll();
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>> expression);
}