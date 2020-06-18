using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public interface ITimeEntryViewModel
    {
        Task<List<Project>> LoadProjects();
        Task<List<Issue>> LoadIssues(int projectId);
    }
}