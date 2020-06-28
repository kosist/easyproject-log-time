using RestSharp.Deserializers;
using AutoMapper;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Autofac;
using EPProvider.Mapping;
using BaseLayer.Model;
using System;
using System.Collections.Generic;
using EPProvider.DTO;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;

namespace APIPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("https://anv.easyproject.cz");

            var request = new RestRequest("time_entries.xml?key=a6531eb52dfbb0076075887bea7aa7bbd94c507b", Method.POST, DataFormat.Xml);
            //request.AddHeader("content-type", "application/xml");
            //request.AddParameter("key", "a6531eb52dfbb0076075887bea7aa7bbd94c507b");

            //request.AddParameter("project_id", "240");

            var timeEntry = new TimeEntryDTO
            {
                ProjectId = 240,
                IssueId = 4770,
                Description = "Test API",
                SpentTime = 2.5,
                SpentOnDate = "2020-06-28"
            };

            request.AddXmlBody(timeEntry);

            var response = client.Post<TimeEntryDTO>(request);

            Console.WriteLine(response.Request.Body);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.StatusDescription);
            //var response = client.Execute<List<TimeEntryXML>>(request);
            //foreach (var timeEntryXml in response.Data)
            //{
            //    Console.WriteLine($"Issue ID: {timeEntryXml.Issue.Id}");
            //}
            //Console.WriteLine(response.ResponseUri);

            //var builder = new ContainerBuilder();
            //builder.AddAutoMapper(typeof(ProjectMapperProfile).Assembly);

            //var container = builder.Build();
            //var mapper = container.Resolve<IMapper>();

            //var timeEntry = new TimeEntry
            //{
            //    ProjectId = 1,
            //    SpentOnDate = DateTime.Now
            //};

            //var timeEntryDto = mapper.Map<TimeEntry, TimeEntryDTO>(timeEntry);

            //Console.WriteLine(timeEntryDto.SpentOnDate);

            //string easyProjectAPIKey = Environment.GetEnvironmentVariable("EasyProjectAPIKey", EnvironmentVariableTarget.User);
            //Console.WriteLine(easyProjectAPIKey);
        }
    }

    [DeserializeAs(Name = "time_entry")]
    public class TimeEntryXML
    {
        public Project Project { get; set; }
        public Issue Issue { get; set; }
    }

    [DeserializeAs(Name = "project")]
    public class Project
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }

    [DeserializeAs(Name = "issue")]
    public class Issue
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }
}
