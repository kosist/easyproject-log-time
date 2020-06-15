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
        private RestClient _client;

        public RestEPProvider(ICredentialsProvider credentialsProvider, IMapper mapper)
        {
            _credentials = credentialsProvider.LoadAPIKey();
            _mapper = mapper;
            _client = new RestClient("https://anv.easyproject.cz");
        }

        public List<Project> GetProjectsList()
        {
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            request.AddParameter("key", _credentials);
            var projects = _client.Execute<List<ProjectDTO>>(request).Data.Select(_mapper.Map<ProjectDTO, Project>).ToList();
            return projects;
        }

        public List<Issue> GetIssuesListForProject(int projectId)
        {
            var request = new RestRequest("issues.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            request.AddParameter("key", _credentials);
            request.AddParameter("limit", 1000);
            request.AddParameter("project_id", projectId);
            var issues = _client.Execute<List<IssueDTO>>(request).Data.Select(_mapper.Map<IssueDTO, Issue>).ToList();
            return issues;
        }

        public void AddTimeEntry(TimeEntry timeEntryData)
        {
            throw new System.NotImplementedException();
        }
    }
}