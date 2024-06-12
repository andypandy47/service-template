using User.Domain.Entities;
using User.Domain.Repositories;

namespace User.Infrastructure.EFCore.Repositories;

public class UserRepository(ApplicationDbContext context) : BaseRepositoryAsync<UserEntity>(context)
{
    
}