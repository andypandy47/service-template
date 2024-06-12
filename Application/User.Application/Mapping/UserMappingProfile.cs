using AutoMapper;
using User.Application.Contracts.DTOs;
using User.Domain.Entities;

namespace User.Application.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, UserEntity>();
    }
}