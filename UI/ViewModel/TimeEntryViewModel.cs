using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
            DisplayProjectsAsync();
        }

        public async Task<List<Project>> LoadProjects()
        {
            return await _provider.GetProjectsListAsync();
        }

        private async Task DisplayProjectsAsync()
        {
            Projects.Clear();
            var projects = await LoadProjects();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }

        public async Task<List<Issue>> LoadIssues(int projectId)
        {
            return await _provider.GetIssuesListForProjectAsync(projectId);
        }

        private async Task DisplayIssuesList(int projectId)
        {
            Issues.Clear();
            var issues = await LoadIssues(projectId);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
        }

        private Project _selectedProject;

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (value != null)
                {
                    _selectedProject = value;
                    OnPropertyChanged();
                    DisplayIssuesList(_selectedProject.Id);
                }
            }
        }

        private float _spentTime;

        public float SpentTime
        {
            get { return _spentTime; }
            set
            {
                _spentTime = ((value >= 0) && (value <= 24)) ? value :  0;
            }
        }



    }
}