using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;

namespace EPProvider
{
    public interface IEPProvider
    {
        Task<(OperationStatusInfo status, List<Project> projectsList)> GetProjectsListAsync();
        Task<List<Issue>> GetIssuesListForProjectAsync(int projectId);
        Task<OperationStatusInfo> AddTimeEntry(TimeEntry timeEntryData);
        Task<OperationStatusInfo> UpdateTimeEntry(TimeEntry timeEntryData);
        Task<OperationStatusInfo> CredentialsValid();
        Task<List<TimeEntry>> GetTimeEntries(DateTime date, int userId);
        Task<(OperationStatusInfo status, int userId)> GetCurrentUserId();
        Task<List<User>> GetProjectUsersListAsync(int projectId);
        Task<OperationStatusInfo> UpdateIssueStatus(UpdatedIssue issue);
        Task<List<User>> GetUsersList();
    }
}