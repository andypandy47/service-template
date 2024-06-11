using AutoMapper;
using User.Application.Contracts.DTOs;
using User.Domain.Core.Entities;

namespace User.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, UserEntity>();
    }
}