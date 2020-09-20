using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }
    }
}