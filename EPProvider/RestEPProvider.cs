using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseLayer.Model;
using EPProvider.DTO;
using RestSharp;
using RestSharp.Authenticators;
using Support.Helper;

namespace EPProvider
{
    public class RestEPProvider : IEPProvider
    {
        private IMapper _mapper;
        private RestClient _client;
        private ICredentialsProvider _credentialsProvider;
        private Credentials _credentials;

        public RestEPProvider(ICredentialsProvider credentialsProvider, IMapper mapper)
        {
            _mapper = mapper;
            _credentialsProvider = credentialsProvider;
            _client = new RestClient("https://anv.easyproject.cz");
            //_client = new RestClient("https://private-anon-b021501636-easyproject.apiary-mock.com/");
            _credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(_credentials.UserName, _credentials.Password);
        }

        public async Task<List<Project>> GetProjectsListAsync()
        {
            Credentials credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            var requestResult = await _client.ExecuteAsync<List<ProjectDTO>>(request); 
            var projects = requestResult.Data.Select(_mapper.Map<ProjectDTO, Project>).ToList();
            return projects;
        }

        public async Task<List<Issue>> GetIssuesListForProjectAsync(int projectId)
        {
            if (projectId > 0)
            {
                Credentials credentials = _credentialsProvider.LoadCredentials();
                _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
                var request = new RestRequest("issues.xml", Method.GET, DataFormat.Xml);
                request.AddHeader("content-type", "application/xml");
                request.AddParameter("limit", 100);
                request.AddParameter("project_id", projectId);
                var offset = 0;
                var requestResult = new List<IssueDTO>();
                var allIssues = new List<IssueDTO>();
                do
                {
                    request.AddOrUpdateParameter("offset", offset);
                    var tasks = await _client.ExecuteAsync<List<IssueDTO>>(request);
                    requestResult = tasks.Data;
                    offset += 100;
                    if (requestResult.Count != 0)
                        allIssues.AddRange(requestResult);
                } while (requestResult.Count != 0);
                
                var issues = allIssues.Select(_mapper.Map<IssueDTO, Issue>).ToList();
                return issues;
            }
            throw new ArgumentOutOfRangeException("Project ID value is invalid!");
        }

        public bool AddTimeEntry(TimeEntry timeEntryData)
        {
            if (timeEntryData.ProjectId <= 0)
                throw new ArgumentOutOfRangeException("Project Id is not valid");
            if (timeEntryData.IssueId <= 0)
                throw new ArgumentOutOfRangeException("Issue Id is not valid");

            bool timeFormatRegexValid = SpentTimeValidation.CheckTimeFormatPattern(timeEntryData.SpentTime);
            bool doubleFormatRegexValid = SpentTimeValidation.CheckDoubleFormatPattern(timeEntryData.SpentTime);
            bool foundGroupSeparator = SpentTimeValidation.CheckGroupSeparator(timeEntryData.SpentTime);
            var validFlag = !foundGroupSeparator && (doubleFormatRegexValid || timeFormatRegexValid);

            if (!validFlag)
                throw new ArgumentException("Spent time has invalid format");

            var timeEntry = _mapper.Map<TimeEntry, TimeEntryDTO>(timeEntryData);
            Credentials credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
            var request = new RestRequest($"time_entries.xml?", Method.POST, DataFormat.Xml);
            request.AddXmlBody(timeEntry);
            var response = _client.Post<TimeEntryDTO>(request);
            var code = (int) response.StatusCode;
            return (code == 201);
        }

        public bool CredentialsValid()
        {
            Credentials credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            var requestResult = _client.Execute<List<ProjectDTO>>(request);
            return requestResult.IsSuccessful;
        }

        public async Task<List<TimeEntry>> GetTimeEntries(DateTime date)
        {
            Credentials credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
            var request = new RestRequest("time_entries.xml?", Method.GET, DataFormat.Xml);
            request.AddParameter("period_type", "2");
            request.AddParameter("from", date.ToString("yyyy-MM-dd"));
            request.AddParameter("to", date.ToString("yyyy-MM-dd"));
            var requestResult = await _client.ExecuteAsync<List<LoggedTimeEntryDTO>>(request);
            var timeEntries = requestResult.Data.Select(_mapper.Map<LoggedTimeEntryDTO, TimeEntry>).ToList();
            return timeEntries;
        }
    }
}