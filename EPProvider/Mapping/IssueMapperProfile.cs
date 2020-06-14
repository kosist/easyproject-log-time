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
                .ForMember(i => i.Parent, opt => opt.MapFrom(src => new ParentDTO
                {
                    Id = src.ParentId,
                }));
            CreateMap<IssueDTO, Issue>()
                .ForMember(i => i.ParentId, opt => opt.MapFrom(src => src.Parent.Id));
        }
    }
}