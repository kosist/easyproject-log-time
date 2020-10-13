using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class UpdatedIssueMockMapperProfile : Profile
    {
        public UpdatedIssueMockMapperProfile()
        {
            CreateMap<UpdatedIssue, UpdatedIssueMockDto>();
            CreateMap<UpdatedIssueMockDto, UpdatedIssue>();
        }
    }
}