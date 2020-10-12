using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;
using EPProvider;

namespace MockProvider
{
    public class MockEpProvider : IEPProvider
    {
        public async Task<(OperationStatusInfo status, List<Project> projectsList)> GetProjectsListAsync()
        {
            throw new NotImplementedException();
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