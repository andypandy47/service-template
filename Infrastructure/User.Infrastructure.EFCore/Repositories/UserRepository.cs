using User.Domain.Core.Entities;
using User.Domain.Core.Repositories;

namespace User.Infrastructure.EFCore.Repositories;

public class UserRepository(ApplicationDbContext context) : BaseRepositoryAsync<UserEntity>(context)
{
    
}