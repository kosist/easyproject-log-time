using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Project>> GetProjectsListAsync()
        {
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            request.AddParameter("key", _credentials);
            var requestResult = await _client.ExecuteAsync<List<ProjectDTO>>(request); 
            var projects = requestResult.Data.Select(_mapper.Map<ProjectDTO, Project>).ToList();
            return projects;
        }

        public async Task<List<Issue>> GetIssuesListForProjectAsync(int projectId)
        {
            if (projectId > 0)
            {
                var request = new RestRequest("issues.xml", Method.GET, DataFormat.Xml);
                request.AddHeader("content-type", "application/xml");
                request.AddParameter("key", _credentials);
                request.AddParameter("limit", 1000);
                request.AddParameter("project_id", projectId);
                var requestResult = await _client.ExecuteAsync<List<IssueDTO>>(request);
                var issues = requestResult.Data.Select(_mapper.Map<IssueDTO, Issue>).ToList();
                return issues;
            }
            throw new ArgumentOutOfRangeException("Project ID value is invalid!");
        }

        public void AddTimeEntry(TimeEntry timeEntryData)
        {
            var timeEntry = _mapper.Map<TimeEntry, TimeEntryDTO>(timeEntryData);
            var request = new RestRequest($"time_entries.xml?key={_credentials}", Method.POST, DataFormat.Xml);
            request.AddXmlBody(timeEntry);
            _client.Post<TimeEntryDTO>(request);
        }
    }
}