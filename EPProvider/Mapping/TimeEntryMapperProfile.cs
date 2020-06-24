using System;
using System.Diagnostics.Tracing;
using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class TimeEntryMapperProfile : Profile
    {
        public TimeEntryMapperProfile()
        {
            CreateMap<TimeEntry, TimeEntryDTO>()
                .ForMember(dest => dest.SpentOnDate, opt => opt.MapFrom(src => src.SpentOnDate.ToString("yyyy-MM-dd")));
            CreateMap<TimeEntryDTO, TimeEntry>()
                .ForMember(dest => dest.SpentOnDate, opt => opt.MapFrom(src => DateTime.Parse(src.SpentOnDate)));
        }
    }
}
