using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class IssueStatusMapperProfile : Profile
    {
        public IssueStatusMapperProfile()
        {
            CreateMap<IssueStatus, IssueStatusDTO>();
            CreateMap<IssueStatusDTO, IssueStatus>();
        }       
    }
}