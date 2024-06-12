using Shared.Kernel.Result;
using User.Application.Contracts.DTOs;
using User.Domain.Entities;

namespace User.Application.Contracts.Interfaces;

public interface IUserService
{
    Task<Result<UserEntity>> Get(Guid id);
    Task<Result<List<UserEntity>>> GetAll(Guid id);
    Task<Result<Guid>> Create(CreateUserDto user);
    Task<Result<Guid>> Update();
    Task<Result> Delete(Guid id);
}