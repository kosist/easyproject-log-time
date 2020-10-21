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

        public async Task<(OperationStatusInfo status, List<Project> projectsList)> GetProjectsListAsync()
        {
            InitHttpBasicAuthenticator();
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            request.AddParameter("limit", 100);
            var requestResult = await _client.ExecuteAsync<List<ProjectDTO>>(request); 
            var projects = requestResult.Data.Select(_mapper.Map<ProjectDTO, Project>).ToList();
            return (GetOperationStatusInfo(requestResult), projects);
        }

        public async Task<List<Issue>> GetIssuesListForProjectAsync(int projectId)
        {
            if (projectId > 0)
            {
                InitHttpBasicAuthenticator();
                var request = new RestRequest("issues.xml", Method.GET, DataFormat.Xml);
                request.AddHeader("content-type", "application/xml");
                request.AddParameter("limit", 100);
                request.AddParameter("project_id", projectId);
                var offset = 0;
                var issuesReceieved = new List<IssueDTO>();
                IRestResponse<List<IssueDTO>> requestResult;
                var allIssues = new List<IssueDTO>();
                do
                {
                    request.AddOrUpdateParameter("offset", offset);
                    requestResult = await _client.ExecuteAsync<List<IssueDTO>>(request);
                    issuesReceieved = requestResult.Data;
                    offset += 100;
                    if (issuesReceieved.Count != 0)
                        allIssues.AddRange(issuesReceieved);
                } while (issuesReceieved.Count != 0);
                
                var issues = allIssues.Select(_mapper.Map<IssueDTO, Issue>).ToList();
                return issues;
            }
            throw new ArgumentOutOfRangeException("Project ID value is invalid!");
        }

        public async Task<OperationStatusInfo> AddTimeEntry(TimeEntry timeEntryData)
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
            InitHttpBasicAuthenticator();
            var request = new RestRequest($"time_entries.xml?", Method.POST, DataFormat.Xml);
            request.AddXmlBody(timeEntry);
            var requestResult = await _client.ExecuteAsync<TimeEntryDTO>(request);
            return GetOperationStatusInfo(requestResult);
        }

        public async Task<OperationStatusInfo> UpdateTimeEntry(TimeEntry timeEntryData)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationStatusInfo> CredentialsValid()
        {
            InitHttpBasicAuthenticator();
            var request = new RestRequest("projects.xml", Method.GET, DataFormat.Xml);
            request.AddHeader("content-type", "application/xml");
            var requestResult = await _client.ExecuteAsync<List<ProjectDTO>>(request);
            return GetOperationStatusInfo(requestResult);
        }

        private static OperationStatusInfo GetOperationStatusInfo(IRestResponse requestResult)
        {
            var requestStatus = requestResult.IsSuccessful;
            var requestStatusMessage = "";
            if (string.IsNullOrEmpty(requestResult.ErrorMessage))
            {
                requestStatusMessage = $"Status is {requestResult.StatusDescription}.";
                if (requestResult.StatusDescription == "Unauthorized")
                    requestStatusMessage =
                        $"Status is {requestResult.StatusDescription}. Check your credentials (login and password).";
            }
            else
            {
                requestStatusMessage = $"Status is {requestResult.ResponseStatus}. Error message: {requestResult.ErrorMessage}";
            }

            return new OperationStatusInfo
            {
                OperationStatus = requestStatus,
                OperationMessage = requestStatusMessage
            };
        }

        public async Task<List<TimeEntry>> GetTimeEntries(DateTime date, int userId)
        {
            InitHttpBasicAuthenticator();
            var request = new RestRequest("time_entries.xml?", Method.GET, DataFormat.Xml);
            request.AddParameter("user_id", userId);
            request.AddParameter("period_type", "2");
            request.AddParameter("from", date.ToString("yyyy-MM-dd"));
            request.AddParameter("to", date.ToString("yyyy-MM-dd"));
            var requestResult = await _client.ExecuteAsync<List<LoggedTimeEntryDTO>>(request);
            var timeEntries = requestResult.Data.Select(_mapper.Map<LoggedTimeEntryDTO, TimeEntry>).ToList();
            return timeEntries;
        }

        public async Task<(OperationStatusInfo status, int userId)> GetCurrentUserId()
        {
            InitHttpBasicAuthenticator();
            
            var request = new RestRequest($"easy_attendances.xml?", Method.GET, DataFormat.Xml);
            var requestResult = await _client.ExecuteAsync<List<AttendanceDTO>>(request);
            var userId = requestResult.Data.First().UserId;
            return (GetOperationStatusInfo(requestResult), userId);
        }

        public async Task<List<User>> GetProjectUsersListAsync(int projectId)
        {
            if (projectId > 0)
            {
                InitHttpBasicAuthenticator();
                var request = new RestRequest($"projects/{projectId}/memberships.xml", Method.GET, DataFormat.Xml);
                request.AddHeader("content-type", "application/xml");
                request.AddParameter("limit", 50);
                var requestResult = await _client.ExecuteAsync<List<UserDTO>>(request);
                var users = requestResult.Data.Select(_mapper.Map<UserDTO, User>).ToList();
                return users;
            }
            throw new ArgumentOutOfRangeException("Project ID value is invalid!");
        }

        public async Task<OperationStatusInfo> UpdateIssueStatus(UpdatedIssue issue)
        {
            var updatedIssue = _mapper.Map<UpdatedIssue, UpdatedIssueDTO>(issue);
            InitHttpBasicAuthenticator();
            var request = new RestRequest($"issues/{issue.Id}.xml", Method.PUT, DataFormat.Xml);
            request.AddXmlBody(updatedIssue);
            var requestResult = await _client.ExecuteAsync<UpdatedIssueDTO>(request);
            return GetOperationStatusInfo(requestResult);
        }

        public async Task<List<User>> GetUsersList()
        {
            var projectsListRequest = await GetProjectsListAsync();
            var projects = projectsListRequest.projectsList;
            var users = new List<User>();
            foreach (var project in projects)
            {
                users.AddRange(await GetProjectUsersListAsync(project.Id));
            }
            return users.Select(user => user).Distinct().ToList();
        }

        private void InitHttpBasicAuthenticator()
        {
            Credentials credentials = _credentialsProvider.LoadCredentials();
            _client.Authenticator = new HttpBasicAuthenticator(credentials.UserName, credentials.Password);
        }
    }
}