using System.Collections.Generic;
using BaseLayer.Model;

namespace EPProvider
{
    public class RestEPProvider : IEPProvider
    {
        private ICredentialsProvider _credentialsProvider;

        public RestEPProvider(ICredentialsProvider credentialsProvider)
        {
            _credentialsProvider = credentialsProvider;
        }

        public List<Project> GetProjectsList()
        {
            throw new System.NotImplementedException();
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