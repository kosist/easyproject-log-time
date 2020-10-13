using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;
using EPProvider;

namespace MockProvider
{
    public class MockEpProvider : IEPProvider
    {
        public List<Project> ProjectsList { get; set; }
        public List<Issue> IssuesList { get; set; }

        public MockEpProvider()
        {
            ProjectsList = new List<Project>();
            ProjectsList.Add(new Project
            {
                Id = 1,
                Name = "2009 AKN 720 6 DUTs"
            });
            ProjectsList.Add(new Project
            {
                Id = 2,
                Name = "2010 Powertrain CU Board"
            });
        }
        public async Task<(OperationStatusInfo status, List<Project> projectsList)> GetProjectsListAsync()
        {
            var operationStatus = new OperationStatusInfo
            {
                OperationMessage = "",
                OperationStatus = true
            };
            
            return (operationStatus, projectsList);
        }

        public async Task<List<Issue>> GetIssuesListForProjectAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationStatusInfo> AddTimeEntry(TimeEntry timeEntryData)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationStatusInfo> CredentialsValid()
        {
            return new OperationStatusInfo
            {
                OperationMessage = "Login is valid",
                OperationStatus = true
            };
        }

        public async Task<List<TimeEntry>> GetTimeEntries(DateTime date, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<(OperationStatusInfo status, int userId)> GetCurrentUserId()
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetProjectUsersListAsync(int projectId)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationStatusInfo> UpdateIssueStatus(UpdatedIssue issue)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetUsersList()
        {
            throw new NotImplementedException();
        }
    }
}