using System.Collections.Generic;
using System.Windows.Documents;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public interface ITimeEntryViewModel
    {
        List<Project> LoadProjects();
        List<Issue> LoadIssues(int projectId);
    }
}