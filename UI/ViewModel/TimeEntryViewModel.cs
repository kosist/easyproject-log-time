using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseLayer.Model;
using EPProvider;
using Prism.Commands;
using Prism.Events;
using UI.Event;
using UI.Wrapper;

namespace UI.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase, ITimeEntryViewModel
    {
        private IEPProvider _provider;
        private IEventAggregator _eventAggregator;
        private TimeEntryWrapper _timeEntry;

        public TimeEntryWrapper TimeEntry
        {
            get { return _timeEntry; }
            set
            {
                _timeEntry = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Issue> Issues { get; private set; }
        public DateTime SpentOnDate { get; set; }

        public ICommand SaveCommand { get; }

        public TimeEntryViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            Projects = new ObservableCollection<Project>();
            Issues = new ObservableCollection<Issue>();
            SpentOnDate = DateTime.Today;
            DisplayProjectsAsync();

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private void OnSaveExecute()
        {
            throw new NotImplementedException();
        }

        private bool OnSaveCanExecute()
        {
            return TimeEntry != null && !TimeEntry.HasErrors;
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

            TimeEntry = new TimeEntryWrapper(new TimeEntryItemViewModel());
            TimeEntry.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TimeEntry.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedProject")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";
                    DisplayIssuesList(TimeEntry.SelectedProject.Id);
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
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
    }
}