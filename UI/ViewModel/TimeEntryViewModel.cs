using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaseLayer.Model;
using EPProvider;

namespace UI.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase, ITimeEntryViewModel
    {
        private IEPProvider _provider;

        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Issue> Issues { get; private set; }

        public TimeEntryViewModel(IEPProvider provider)
        {
            _provider = provider;
            Projects = new ObservableCollection<Project>();
            Issues = new ObservableCollection<Issue>();
            LoadProjects();
            LoadIssues(240);
        }

        public void LoadProjects()
        {
            Projects.Clear();
            var projects = _provider.GetProjectsList();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        public void LoadIssues(int projectId)
        {
            Issues.Clear();
            var issues = _provider.GetIssuesListForProject(projectId);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
        }
    }
}