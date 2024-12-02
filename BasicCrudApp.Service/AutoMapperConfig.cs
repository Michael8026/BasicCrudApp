using AutoMapper;
using BasicCrudApp.Domain;
using BasicCrudApp.Domain.Entities;

namespace BasicCrudApp.Services
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<User, CreateUserDto>()
                .ReverseMap();
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
