using AutoMapper;
using backend.Dto;
using backend.Models;

namespace backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto2>();
        CreateMap<UserDto2, User>();
        CreateMap<Registry, RegistryDto>();
        CreateMap<RegistryDto, Registry>();
        CreateMap<UserDetailDto, Student>();
        CreateMap<UserDetailDto, Teacher>();
        CreateMap<Student, UserDetailDto>();
        CreateMap<Teacher, UserDetailDto>();
    }
}