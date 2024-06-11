using Microsoft.EntityFrameworkCore;
using Shared.Domain.Result;
using User.Application.Contracts.DTOs;
using User.Application.Contracts.Interfaces;
using User.Domain.Core.Entities;
using User.Domain.Core.Repositories;

namespace User.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<UserEntity>> Get(Guid id)
    {
        var repository = _unitOfWork.Repository<UserEntity>();
        var user = await repository.GetByIdAsync(id);

        if (user is null)
        {
            return UserErrors.NotFound(id);
        }
        
        return Result.From(user);
    }

    public async Task<Result<List<UserEntity>>> GetAll(Guid id)
    {
        var repository = _unitOfWork.Repository<UserEntity>();

        var allUsers = await repository
            .GetAll()
            .ToListAsync();

        return Result.From(allUsers);
    }

    public async Task<Result<Guid>> Create(CreateUserDto createUser)
    {
        var repository = _unitOfWork.Repository<UserEntity>();

        await _unitOfWork.BeginTransactionAsync();
        
        //Do some stuff

        await _unitOfWork.CommitTransactionAsync();

        return Result.From(Guid.NewGuid());
    }

    public async Task<Result<Guid>> Update()
    {
        var repository = _unitOfWork.Repository<UserEntity>();

        await _unitOfWork.BeginTransactionAsync();
        
        //Do some stuff

        await _unitOfWork.CommitTransactionAsync();

        return Result.From(Guid.NewGuid());
    }

    public async Task<Result> Delete(Guid id)
    {
        var repository = _unitOfWork.Repository<UserEntity>();

        var entity = await repository.GetByIdAsync(id);

        if (entity is null)
        {
            return UserErrors.NotFound(id);
        }
        
        repository.Delete(entity);
        
        return Result.Success;
    }
}