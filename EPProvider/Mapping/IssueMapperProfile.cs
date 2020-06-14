using System.Diagnostics.Tracing;
using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class IssueMapperProfile : Profile
    {
        public IssueMapperProfile()
        {
            CreateMap<Issue, IssueDTO>()
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => new ParentDTO
                {
                    Id = src.ParentId,
                }));
            CreateMap<IssueDTO, Issue>()
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.Parent.Id));
        }
    }
}