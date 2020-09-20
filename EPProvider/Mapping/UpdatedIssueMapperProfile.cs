using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class UpdatedIssueMapperProfile : Profile
    {
        public UpdatedIssueMapperProfile()
        {
            CreateMap<UpdatedIssue, UpdatedIssueDTO>();
            CreateMap<UpdatedIssueDTO, UpdatedIssue>();
        }
    }
}