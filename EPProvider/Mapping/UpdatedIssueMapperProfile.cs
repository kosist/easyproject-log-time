using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class UpdatedIssueMapperProfile : Profile
    {
        public UpdatedIssueMapperProfile()
        {
            CreateMap<UpdatedIssue, UpdatedIssueDTO>()
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status.Id));
            CreateMap<UpdatedIssueDTO, UpdatedIssue>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}