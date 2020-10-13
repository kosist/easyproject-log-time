using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class ProjectMockMapperProfile : Profile
    {
        public ProjectMockMapperProfile()
        {
            CreateMap<Project, ProjectMockDto>()
                .ForMember(dest => dest.Issues, opt => opt.Ignore());
            CreateMap<ProjectMockDto, Project>();
        }
    }
}
