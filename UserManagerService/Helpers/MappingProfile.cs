using AutoMapper;
using UserManagerService.Dtos;
using UserManagerService.Models;

namespace UserManagerService.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
