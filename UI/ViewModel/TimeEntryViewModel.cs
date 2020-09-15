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
        #region Private Properties

        private IEPProvider _provider;
        private IEventAggregator _eventAggregator;

        #endregion

        #region Public Properties

        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Issue> Issues { get; private set; }
        public ObservableCollection<IssueItemViewModel> Nodes { get; private set; }
        public ObservableCollection<IssueItemViewModel> Tasks { get; private set; }
        public DateTime SpentOnDate { get; set; }
        public ICommand SaveCommand { get; }
        public int CurrentUserId { get; private set; }

        #endregion

        #region Constructor
        public TimeEntryViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            Projects = new ObservableCollection<Project>();
            Issues = new ObservableCollection<Issue>();
            Nodes = new ObservableCollection<IssueItemViewModel>();
            Tasks = new ObservableCollection<IssueItemViewModel>();

            _eventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginSuccessEvent);
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);

            LoggedTime = 0;
            SpentOnDate = DateTime.Today;
        }       

        #endregion
        
        #region Private helper methods

        /// <summary>
        /// Waits for LoginSuccessEvent. If status is true, then project's list is loaded
        /// </summary>
        /// <param name="status">Status of the login operation</param>
        private async void OnLoginSuccessEvent(bool status)
        {
            if (status)
            {
                await DisplayProjectsAsync();
                CurrentUserId = await _provider.GetCurrentUserId();
            }
                

        }
        private void InitTimeEntry()
        {
            TimeEntry = new TimeEntryWrapper(new TimeEntryItemViewModel());
            TimeEntry.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(TimeEntry.HasErrors))
                {
                    ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedProject")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";
                    DisplayIssuesList(TimeEntry.SelectedProject.Id);
                }

                if (e.PropertyName == "SpentOnDate")
                {
                    var timeEntries = await _provider.GetTimeEntries(TimeEntry.SpentOnDate, CurrentUserId);
                    LoggedTime = CalculateLoggedTime(timeEntries);
                }
            };
            ((DelegateCommand) SaveCommand).RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Method returns sum of logged hours
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        private double CalculateLoggedTime(List<TimeEntry> timeEntries)
        {
            if (timeEntries.Count == 0)
                return 0;
            var sum = 0.0;
            var result = 0.0;
            var validRecord = false;
            foreach (var timeEntry in timeEntries)
            {
                validRecord = double.TryParse(timeEntry.SpentTime, out result);
                sum += result;
            }
            return Math.Round(sum, 2);
        }

        #endregion
        
        #region Save Methods

                /// <summary>
        /// When Save button is pressed, TimeEntry data is written to EP
        /// </summary>
        private async void OnSaveExecute()
        {
            var timeEntry = new TimeEntry
            {
                ProjectId = TimeEntry.SelectedProject.Id,
                IssueId = TimeEntry.SelectedIssue.Id,
                SpentOnDate = TimeEntry.SpentOnDate,
                Description = TimeEntry.Description,
                SpentTime = TimeEntry.SpentTime,
            };
            //var result = _provider.AddTimeEntry(timeEntry);
            //if (!result)
            //    throw new Exception("Post method executed with error!");

            InitTimeEntry();
            var timeEntries = await _provider.GetTimeEntries(TimeEntry.SpentOnDate, CurrentUserId);
            LoggedTime = CalculateLoggedTime(timeEntries);
        }

        /// <summary>
        /// Check, whether Save button could be pressed (in other words, whether save operation could be executed
        /// </summary>
        /// <returns></returns>
        private bool OnSaveCanExecute()
        {
            return TimeEntry != null && !TimeEntry.HasErrors;
        }

        #endregion

        #region UI methods

        /// <summary>
        /// Loads list of projects from the EasyProject
        /// </summary>
        /// <returns></returns>
        public async Task<List<Project>> LoadProjects()
        {
            return await _provider.GetProjectsListAsync();
        }

        /// <summary>
        /// Displays list of project on the UI. List is cleared, and then projects are added to the list one-by-one
        /// </summary>
        /// <returns></returns>
        private async Task DisplayProjectsAsync()
        {
            Projects.Clear();
            var projects = await LoadProjects();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }

            InitTimeEntry();
        }

        /// <summary>
        /// Retrieves list of issues for the selected project
        /// </summary>
        /// <param name="projectId">ID of the selected project</param>
        /// <returns></returns>
        public async Task<List<Issue>> LoadIssues(int projectId)
        {
            return await _provider.GetIssuesListForProjectAsync(projectId);
        }
        /// <summary>
        /// Helper function which initializes TimeEntry with listening to PropertyChanged events
        /// </summary>

        /// <summary>
        /// Displays list of issues for the selected project
        /// </summary>
        /// <param name="projectId">ID of the selected project</param>
        /// <returns></returns>
        private async Task DisplayIssuesList(int projectId)
        {
            Issues.Clear();
            var issues = await LoadIssues(projectId);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
            BuildTreeAndGetRoots(Issues);
        }       

        #endregion
        
        #region Full Properties

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

        private double _loggedTime;

        public double LoggedTime
        {
            get { return _loggedTime; }
            set
            {
                _loggedTime = value;
                OnPropertyChanged();
            }
        }


        #endregion

        #region Tree View

        public class Node
        {
            public List<Node> Children = new List<Node>();
            public Node Parent { get; set; }
            public Issue AssociatedObject { get; set; }
            public string Name { get; set; }
        }

        public void BuildTreeAndGetRoots(ObservableCollection<Issue> actualObjects)
        {
            Nodes.Clear();
            Dictionary<int, IssueItemViewModel> lookup = new Dictionary<int, IssueItemViewModel>();

            foreach (var issue in actualObjects)
            {
                lookup.Add(issue.Id, new IssueItemViewModel
                {
                    AssociatedObject = issue,
                    Name = issue.Name,
                    SelectedName = issue.Name,
                    Level = 0,
                });
            }

            //actualObjects.ForEach(x => lookup.Add(x.Id, new Node { AssociatedObject = x }));
            foreach (var item in lookup.Values)
            {
                IssueItemViewModel proposedParent;
                if (lookup.TryGetValue(item.AssociatedObject.ParentId, out proposedParent))
                {
                    item.Parent = proposedParent;
                    proposedParent.Children.Add(item);
                }
            }
            //return lookup.Values.Where(x => x.Parent == null);
            foreach (var node in lookup)
            {
                if (node.Value.Parent == null)
                {
                    Nodes.Add(node.Value);
                }
            }
            Tasks.Clear();
            UpdateLevel(Nodes);
            IndentNames(Nodes);
        }

        private void IndentNames(ObservableCollection<IssueItemViewModel> nodes)
        {
            
            foreach (var node in nodes)
            {
                var item = new IssueItemViewModel();
                item.AssociatedObject = node.AssociatedObject;
                item.Parent = node.Parent;
                item.Level = node.Level;
                item.Name = node.Name;
                item.SelectedName = node.SelectedName;
                foreach (var child in node.Children)
                {
                    item.Children.Add(child);
                }
                for (int i = 0; i < item.Level; i++)
                {
                    item.Name = "   " + item.Name;
                }
                Tasks.Add(item);
                IndentNames(new ObservableCollection<IssueItemViewModel>(node.Children));
            }
        }

        private void UpdateLevel(ObservableCollection<IssueItemViewModel> items)
        {
            foreach (var item in items)
            {
                if (item.Parent != null)
                {
                    if (item.Level != item.Parent.Level + 1)
                    {
                        item.Level = item.Parent.Level + 1;
                    }
                }
                else
                {
                    item.Level++;
                }
                UpdateLevel(item.Children);
            }
        }

        #endregion
    }
}