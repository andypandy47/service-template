using User.Domain.Entities;

namespace User.Domain.Repositories;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
    Task<int> SaveChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}