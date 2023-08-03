using AutoMapper;
using backend.DTO;
using backend.Models;

namespace backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDTO>();
    }
}