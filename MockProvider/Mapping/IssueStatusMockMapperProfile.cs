using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class IssueStatusMockMapperProfile : Profile
    {
        public IssueStatusMockMapperProfile()
        {
            CreateMap<IssueStatus, IssueStatusMockDto>();
            CreateMap<IssueStatusMockDto, IssueStatus>();
        }
    }
}