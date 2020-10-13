using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class UserMockMapperProfile : Profile
    {
        public UserMockMapperProfile()
        {
            CreateMap<User, UserMockDto>();
            CreateMap<UserMockDto, User>();
        }
    }
}
