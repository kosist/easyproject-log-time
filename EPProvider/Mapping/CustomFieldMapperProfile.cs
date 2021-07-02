using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class CustomFieldMapperProfile : Profile
    {
        public CustomFieldMapperProfile()
        {
            CreateMap<CustomField, CustomFieldDTO>();
            CreateMap<CustomFieldDTO, CustomField>();
        }
    }
}
