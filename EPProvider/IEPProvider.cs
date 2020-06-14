using System.Collections.Generic;
using BaseLayer.Model;

namespace EPProvider
{
    public interface IEPProvider
    {
        List<Project> GetProjectsList();
        List<Issue> GetIssuesListForProject(int projectId);
        void AddTimeEntry(TimeEntry timeEntryData);
    }
}