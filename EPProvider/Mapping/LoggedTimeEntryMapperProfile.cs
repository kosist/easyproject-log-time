using System;
using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;

namespace EPProvider.Mapping
{
    public class LoggedTimeEntryMapperProfile : Profile
    {
        public LoggedTimeEntryMapperProfile()
        {
            CreateMap<TimeEntry, LoggedTimeEntryDTO>()
                .ForMember(dest => dest.Project, opt => opt.MapFrom(src => new ProjectDTO {Id = src.ProjectId}))
                .ForMember(dest => dest.Issue, opt => opt.MapFrom(src => new IssueDTO {Id = src.IssueId}))
                .ForMember(dest => dest.SpentOnDate, opt => opt.MapFrom(src => src.SpentOnDate.ToString("yyyy-MM-dd")));
                //.ForMember(dest => dest.User, src => src.Ignore())
                //.ForMember(dest => dest.Id, src => src.Ignore());
            CreateMap<LoggedTimeEntryDTO, TimeEntry>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Project.Id))
                .ForMember(dest => dest.IssueId, opt => opt.MapFrom(src => src.Issue.Id))
                .ForMember(dest => dest.SpentOnDate, opt => opt.MapFrom(src => DateTime.Parse(src.SpentOnDate)));
        }
    }
}