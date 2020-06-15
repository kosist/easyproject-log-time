using System.Collections.Generic;
using System.Windows.Documents;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public interface ITimeEntryViewModel
    {
        void LoadProjects();
        void LoadIssues(int projectId);
    }
}