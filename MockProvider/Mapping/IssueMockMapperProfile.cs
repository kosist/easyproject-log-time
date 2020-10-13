using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class IssueMockMapperProfile : Profile
    {
        public IssueMockMapperProfile()
        {
            CreateMap<Issue, IssueMockDto>()
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => new IssueParentMockDto()
                {
                    Id = src.ParentId,
                }));
            CreateMap<IssueMockDto, Issue>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Parent.Id));
        }
    }
}