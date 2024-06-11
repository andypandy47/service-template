using User.Domain.Core.Entities;

namespace User.Domain.Service;

public interface IUserService
{
    Task<global::User.Domain.Core.Entities.UserEntity> Get(Guid id);
}