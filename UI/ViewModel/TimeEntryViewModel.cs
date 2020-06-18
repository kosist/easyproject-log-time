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
            DisplayProjects();
            DisplayIssuesList(240);
        }

        public List<Project> LoadProjects()
        {
            return _provider.GetProjectsList();
        }

        private void DisplayProjects()
        {
            Projects.Clear();
            var projects = LoadProjects();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        public List<Issue> LoadIssues(int projectId)
        {
            return _provider.GetIssuesListForProject(projectId);
        }

        private void DisplayIssuesList(int projectId)
        {
            Issues.Clear();
            var issues = LoadIssues(projectId);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
        }
    }
}