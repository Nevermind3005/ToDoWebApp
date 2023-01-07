using AutoMapper;
using server.Models;

namespace server.Profiles;

public class ToDoProfile: Profile
{
    public ToDoProfile()
    {
        CreateMap<ToDo, ToDoGetDto>();
        CreateMap<ToDoAddDto, ToDo>();
        CreateMap<ToDoUpdateDto, ToDo>();
    }
}