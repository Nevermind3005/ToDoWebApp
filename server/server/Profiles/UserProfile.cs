using AutoMapper;
using server.Models;

namespace server.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserAddDto, User>();
        CreateMap<UserRegisterDto, User>();
        CreateMap<User, UserGetDto>();
    }
}