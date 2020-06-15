using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;
using RestSharp;

namespace EPProvider
{
    public class RestEPProvider : IEPProvider
    {
        private string _credentials;
        private IMapper _mapper;

        public RestEPProvider(ICredentialsProvider credentialsProvider, IMapper mapper)
        {
            _credentials = credentialsProvider.LoadAPIKey();
            _mapper = mapper;
        }

        public List<Project> GetProjectsList()
        {
            var client = new RestClient("https://anv.easyproject.cz");

            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            request.AddParameter("key", _credentials);
            var projects = client.Execute<List<ProjectDTO>>(request).Data.Select(_mapper.Map<ProjectDTO, Project>).ToList();
            return projects;
        }

        public List<Issue> GetIssuesListForProject(int projectId)
        {
            throw new System.NotImplementedException();
        }

        public void AddTimeEntry(TimeEntry timeEntryData)
        {
            throw new System.NotImplementedException();
        }
    }
}