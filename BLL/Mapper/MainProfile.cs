using AutoMapper;
using BLL.DTOs.User;
using DAL.Entities;

namespace BLL.Mapper;

public class MainProfile : Profile
{
    public MainProfile()
    {
        /*
        CreateMap<Project, ProjectDTO>();
        CreateMap<ProjectDTO, Project>();
        CreateMap<Task, TaskDTO>();
        CreateMap<TaskDTO, Task>();
        */

        CreateMap<User, UserMainDataDTO>();


    }
}