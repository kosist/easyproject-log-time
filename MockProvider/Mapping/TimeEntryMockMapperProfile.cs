using System;
using AutoMapper;
using BaseLayer.Model;

namespace MockProvider
{
    public class TimeEntryMockMapperProfile : Profile
    {
        public TimeEntryMockMapperProfile()
        {
            CreateMap<TimeEntry, TimeEntryMockDto>();
            CreateMap<TimeEntryMockDto, TimeEntry>();
        }
    }
}