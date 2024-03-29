﻿using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using BaseLayer.Model;
using EPProvider;
using Microsoft.VisualBasic.FileIO;
using Prism.Commands;
using Prism.Events;
using UI.Event;
using UI.Wrapper;
using System.Diagnostics;
using UI.Support;

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
        public ObservableCollection<User> Users { get; private set; }
        public DoneRatioList DoneRatioList { get; private set; }
        public TaskStatusesViewModel TaskStatuses { get; private set; }
        public UserSelectedEvent SelectedUserEventPublisher { get; set; }
        public TimeLogsUpdatedEvent TimeLogsUpdatedEventPublisher { get; set; }
        public DateTime SpentOnDate { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand OpenProjectCommand { get; }
        public ICommand AddTaskCommand { get; }
        public ICommand OpenTaskCommand { get; }
        public int CurrentUserId { get; private set; }
        private TimeEntry _timeEntryToUpdate { get; set; }
        public DataUpdatedEvent DataUpdatedEvent { get; }
        public SelectLogHoursTabEvent SelectLogHoursTabEvent { get; }

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
            Users = new ObservableCollection<User>();
            DoneRatioList = new DoneRatioList();
            TaskStatuses = new TaskStatusesViewModel();

            _eventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginSuccessEvent);
            _eventAggregator.GetEvent<EditTimeEntryEvent>().Subscribe(OnEditTimeEntryEvent);
            _eventAggregator.GetEvent<CopyTimeEntryEvent>().Subscribe(OnCopyTimeEntryEvent);

            SelectedUserEventPublisher = _eventAggregator.GetEvent<UserSelectedEvent>();
            TimeLogsUpdatedEventPublisher = _eventAggregator.GetEvent<TimeLogsUpdatedEvent>();
            DataUpdatedEvent = _eventAggregator.GetEvent<DataUpdatedEvent>();
            SelectLogHoursTabEvent = _eventAggregator.GetEvent<SelectLogHoursTabEvent>();

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            CancelCommand = new DelegateCommand(OnCancelExecute, OnCancelCanExecute);
            OpenProjectCommand = new DelegateCommand(OnOpenProjectExecute, OnOpenProjectCanExecute);
            AddTaskCommand = new DelegateCommand(OnAddTaskExecute, OnAddTaskCanExecute);
            OpenTaskCommand = new DelegateCommand(OnOpenTaskExecute, OnOpenTaskCanExecute);

            SaveOption = true;
            UpdateOption = false;

            LoggedTime = 0;
            SpentOnDate = DateTime.Today;
        }

        private bool OnOpenTaskCanExecute()
        {
            if (TimeEntry != null)
                return TimeEntry.SelectedIssue != null;
            else
                return false;
        }

        private void OnOpenTaskExecute()
        {
            try
            {
                if (TimeEntry.SelectedIssue != null)
                {
                    var link = $"https://anv.easyproject.cz/issues/{TimeEntry.SelectedIssue.Id}";
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = link,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool OnAddTaskCanExecute()
        {
            if (TimeEntry != null)
                return TimeEntry.SelectedProject != null;
            else
                return false;
        }

        private void OnAddTaskExecute()
        {
            try
            {
                if (TimeEntry.SelectedProject != null)
                {
                    var link = $"https://anv.easyproject.cz/projects/{TimeEntry.SelectedProject.Id}/issues/new?utm_campaign=menu&utm_content=easy_new_entity&utm_term=issues_new";
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = link,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void OnOpenProjectExecute()
        {
            try
            {
                if (TimeEntry.SelectedProject != null)
                {
                    var link = $"https://anv.easyproject.cz/projects/{TimeEntry.SelectedProject.Id}";
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = link,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool OnOpenProjectCanExecute()
        {
            if (TimeEntry != null)
                return TimeEntry.SelectedProject != null;
            else
                return false;
        }

        #endregion

        #region Event Handlers

        private void OnEditTimeEntryEvent(TimeEntry timeEntry)
        {
            SaveOption = false;
            UpdateOption = true;
            _timeEntryToUpdate = timeEntry;
            TimeEntry.SelectedProject = Projects.Single(proj => proj.Id == timeEntry.ProjectId);
        }

        private void OnCopyTimeEntryEvent(TimeEntry timeEntry)
        {
            SaveOption = true;
            UpdateOption = false;
            timeEntry.Id = 0;
            _timeEntryToUpdate = timeEntry;
            TimeEntry.SelectedProject = Projects.Single(proj => proj.Id == timeEntry.ProjectId);
        }

        private bool OnCancelCanExecute()
        {
            return true;
        }

        private void OnCancelExecute()
        {
            if (TimeEntry != null)
                TimeEntry.SelectedProject = null;
        }

        #endregion


        #region Private helper methods

        private async Task<List<Project>> FilterProjects()
        {
            var projects = await LoadProjects();
            var filteredProjects = await ProcessProjects.FilterProjects(projects, new List<string>
            {
                "Realization",
                "Presale"
            });
            return filteredProjects;
        }

        /// <summary>
        /// Waits for LoginSuccessEvent. If status is true, then project's list is loaded
        /// </summary>
        /// <param name="status">Status of the login operation</param>
        private async void OnLoginSuccessEvent(bool status)
        {
            if (status)
            {
                await DisplayProjectsAsync();
                //var requestResult = await _provider.GetCurrentUserId();
                CurrentUserId = 93;
                SelectedUserEventPublisher.Publish(CurrentUserId);
                await GetLoggedTime();
            }            
        }

        private async Task GetLoggedTime()
        {
            var userId = CurrentUserId;
            if (TimeEntry.SelectedUser != null)
            {
                userId = TimeEntry.SelectedUser.Id;
            }
            var timeEntries = await _provider.GetTimeEntries(TimeEntry.SpentOnDate, userId);
            LoggedTime = CalculateLoggedTime(timeEntries);
        }

        private async Task InitTimeEntry()
        {
            TimeEntry = new TimeEntryWrapper(new TimeEntryItemViewModel());
            TimeEntry.PropertyChanged += async (s, e) =>
            {
                if (e.PropertyName == nameof(TimeEntry.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedProject")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";

                    if (TimeEntry.SelectedProject == null)
                    {
                        TimeEntry.Id = 0;
                        TimeEntry.SelectedIssue = null;
                        TimeEntry.SelectedUser = null;
                        TimeEntry.Description = "";
                        TimeEntry.SpentTime = "";
                        TimeEntry.SpentOnDate = DateTime.Today;
                        Issues.Clear();
                        Tasks.Clear();
                        Users.Clear();
                        SaveOption = true;
                        UpdateOption = false;
                        _timeEntryToUpdate = null;
                    }
                    else if(Projects.SingleOrDefault(proj => proj.Name == TimeEntry.SelectedProject.Name) != null)
                    {
                        await DisplayIssuesList(TimeEntry.SelectedProject.Id, _timeEntryToUpdate?.IssueId);
                        await DisplayUsersListAsync(TimeEntry.SelectedProject.Id, _timeEntryToUpdate?.UserId);
                    }
                    ((DelegateCommand)OpenProjectCommand).RaiseCanExecuteChanged();
                    ((DelegateCommand)AddTaskCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedIssue")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";
                    
                    UpdateTask = false;
                    if (TimeEntry.SelectedIssue != null)
                    {
                        TaskStatuses.TaskStatus =
                            TaskStatuses.Statuses.First(task => task.Id == TimeEntry.SelectedIssue.Status.Id);
                        EstimatedTime = TimeEntry.SelectedIssue.EstimatedHours;
                    }
                    else
                        TaskStatuses.TaskStatus = null;
                    if (_timeEntryToUpdate != null)
                    {
                        TimeEntry.Description = _timeEntryToUpdate.Description;
                        TimeEntry.SpentOnDate = _timeEntryToUpdate.SpentOnDate;
                        var culture = CultureInfo.CurrentCulture;
                        var separator = culture.NumberFormat.NumberDecimalSeparator;
                        var currentSeparator = "";
                        if (separator == ".")
                        {
                            currentSeparator = ",";
                        }
                        else
                        {
                            currentSeparator = ".";
                        }
                        if (!_timeEntryToUpdate.SpentTime.Contains(separator))
                        {
                            TimeEntry.SpentTime = _timeEntryToUpdate.SpentTime.Replace(currentSeparator, separator);
                        }
                        else
                        {
                            TimeEntry.SpentTime = _timeEntryToUpdate.SpentTime;
                        }
                        
                        TimeEntry.Id = _timeEntryToUpdate.Id;
                    }
                    ((DelegateCommand)OpenTaskCommand).RaiseCanExecuteChanged();
                }

                if ((e.PropertyName == "SpentOnDate") || (e.PropertyName == "SelectedUser"))
                {
                    await GetLoggedTime();
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
                validRecord = double.TryParse(timeEntry.SpentTime.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator), out result);
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
            if (TimeEntry.SelectedUser == null)
            {
                TimeEntry.SelectedUser = new User
                {
                    Id = CurrentUserId
                };
            }
            var timeEntry = new TimeEntry
            {
                Id = TimeEntry.Id,
                ProjectId = TimeEntry.SelectedProject.Id,
                IssueId = TimeEntry.SelectedIssue.Id,
                SpentOnDate = TimeEntry.SpentOnDate,
                Description = TimeEntry.Description,
                SpentTime = TimeEntry.SpentTime,
                UserId = TimeEntry.SelectedUser.Id,
            };
            OperationStatusInfo result;
            if (timeEntry.Id != 0)
            {
                result = await _provider.UpdateTimeEntry(timeEntry);
            }
            else
            {
                result = await _provider.AddTimeEntry(timeEntry);
            }
            
            if (!result.OperationStatus)
                throw new Exception("Post method executed with error!");
            
            if (UpdateTask)
            {
                await _provider.UpdateIssueStatus(new UpdatedIssue
                {
                    Id = TimeEntry.SelectedIssue.Id,
                    Status = TaskStatuses.TaskStatus,
                    DoneRatio = TimeEntry.SelectedIssue.DoneRatio
                });
            }

            TimeEntry.Description = "";
            TimeEntry.SpentTime = "";
            TimeEntry.Id = 0;
            SaveOption = true;
            UpdateOption = false;
            _timeEntryToUpdate = null;

            await GetLoggedTime();
            TimeLogsUpdatedEventPublisher.Publish();
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
            var requestResult = await _provider.GetProjectsListAsync();
            return requestResult.projectsList;
        }

        /// <summary>
        /// Displays list of project on the UI. List is cleared, and then projects are added to the list one-by-one
        /// </summary>
        /// <returns></returns>
        private async Task DisplayProjectsAsync()
        {
            Projects.Clear();
            List<Project> filteredProjects = await FilterProjects();
            foreach (var project in filteredProjects)
            {
                Projects.Add(project);
            }
            await InitTimeEntry();
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
        /// <param name="issueId">ID of default issue to be selected</param>
        /// <returns></returns>
        private async Task DisplayIssuesList(int projectId, int? issueId = 0)
        {
            Issues.Clear();
            var issues = await LoadIssues(projectId);
            foreach (var issue in issues)
            {
                if (ActiveTasks)
                {
                    if (issue.Status.Id != 4)
                        Issues.Add(issue);
                }
                else
                {
                    Issues.Add(issue);
                }
            }
            
            BuildTreeAndGetRoots(Issues, issueId);
        }

        private async Task DisplayUsersListAsync(int projectId, int? selectedUserId = 0)
        {
            Users.Clear();
            var users = await _provider.GetProjectUsersListAsync(projectId);
            foreach (var user in users)
            {
                Users.Add(user);
            }

            if ((selectedUserId > 0) && (TimeEntry != null))
                TimeEntry.SelectedUser = Users.FirstOrDefault(user => user.Id == selectedUserId);
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

        private double _estimatedTime;

        public double EstimatedTime
        {
            get { return _estimatedTime; }
            set 
            { 
                _estimatedTime = value;
                OnPropertyChanged();
            }
        }


        private bool _activeTasks;

        public bool ActiveTasks
        {
            get { return _activeTasks; }
            set
            {
                _activeTasks = value;
                if (TimeEntry != null)
                    if (TimeEntry.SelectedProject.Id !=0 )
                        DisplayIssuesList(TimeEntry.SelectedProject.Id);
            }
        }

        private bool _updateTask;

        public bool UpdateTask
        {
            get { return _updateTask; }
            set
            {
                _updateTask = value;
                OnPropertyChanged();
            }
        }

        private IssueStatus _taskStatus;

        public IssueStatus TaskStatus
        {
            get { return _taskStatus; }
            set
            {
                _taskStatus = value;
                UpdateTask = true;
                if (TimeEntry.SelectedIssue != null)
                {
                    if (TimeEntry.SelectedIssue.Status != _taskStatus)
                        TimeEntry.SelectedIssue.Status = value;
                }
                OnPropertyChanged();
            }
        }

        private bool _saveOption;

        public bool SaveOption
        {
            get { return _saveOption; }
            set
            {
                _saveOption = value; 
                OnPropertyChanged();
            }
        }

        private bool _updateOption;

        public bool UpdateOption
        {
            get { return _updateOption; }
            set
            {
                _updateOption = value; 
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

        public void BuildTreeAndGetRoots(ObservableCollection<Issue> actualObjects, int? issueId = 0)
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
                    IssueId = issue.Id,
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
            IndentNames(Nodes, issueId);
        }

        private void IndentNames(ObservableCollection<IssueItemViewModel> nodes, int? issueId = 0)
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
            if ((issueId > 0) && (TimeEntry != null))
                TimeEntry.SelectedIssue = Issues.FirstOrDefault(issue => issue.Id == issueId);
            DataUpdatedEvent.Publish(true);
            SelectLogHoursTabEvent.Publish();
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