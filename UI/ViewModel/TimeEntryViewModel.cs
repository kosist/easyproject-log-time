using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BaseLayer.Model;
using EPProvider;
using Prism.Events;
using UI.Event;

namespace UI.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase, ITimeEntryViewModel
    {
        private IEPProvider _provider;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Issue> Issues { get; private set; }
        public DateTime SpentOnDate { get; set; }

        public TimeEntryViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            Projects = new ObservableCollection<Project>();
            Issues = new ObservableCollection<Issue>();
            SpentOnDate = DateTime.Today;
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
                    SelectedIssue = null;
                    Comment = "";
                    DisplayIssuesList(_selectedProject.Id);
                }
            }
        }

        private Issue _selectedIssue;

        public Issue SelectedIssue
        {
            get { return _selectedIssue; }
            set
            {
                _selectedIssue = value;
                OnPropertyChanged();
            }
        }


        private string _spentTime;

        public string SpentTime
        {
            get { return _spentTime; }
            set
            {
                _spentTime = value;
                OnPropertyChanged();
            }
        }

        private string _comment;

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
        }
    }
}