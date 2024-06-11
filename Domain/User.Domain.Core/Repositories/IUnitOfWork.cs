using User.Domain.Core.Entities;

namespace User.Domain.Core.Repositories;

public interface IUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollBackTransactionAsync();
    Task<int> SaveChangesAsync();
    IBaseRepositoryAsync<T> Repository<T>() where T : BaseEntity;
}