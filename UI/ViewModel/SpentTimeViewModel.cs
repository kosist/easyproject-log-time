using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BaseLayer.Model;
using EPProvider;
using Prism.Events;
using UI.Event;

namespace UI.ViewModel
{
    public class SpentTimeViewModel : ViewModelBase, ISpentTimeViewModel
    {
        private IEventAggregator _eventAggregator;
        private IEPProvider _provider;
        public ObservableCollection<SpentTimeRecordViewModel> SpentTimeRecords { get; private set; }
        public ObservableCollection<User> UsersList { get; private set; }
        public Dictionary<int, List<Issue>> IssuesLookup { get; set; }

        public SpentTimeViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            SpentTimeRecords = new ObservableCollection<SpentTimeRecordViewModel>();
            UsersList = new ObservableCollection<User>();
            SpentOnDate = DateTime.Today;
            IssuesLookup = new Dictionary<int, List<Issue>>();
            _eventAggregator.GetEvent<UserSelectedEvent>().Subscribe(OnUserSelectedEvent);
        }

        private async void OnUserSelectedEvent(int userId)
        {
            await InitUsersList();
            if (UsersList.Count != 0)
                SelectedUser = UsersList.Single(user => user.Id == userId);
        }

        public Task<List<TimeEntry>> LoadTimeEntries(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> LoadUsersList()
        {
            return await _provider.GetUsersList();
        }

        private async Task InitUsersList()
        {
            var users = await LoadUsersList();
            UsersList.Clear();
            foreach (var user in users)
            {
                UsersList.Add(user);
            }
        }

        private async Task<List<Project>> LoadProjects()
        {
            var projects = await _provider.GetProjectsListAsync();
            return projects.projectsList;
        }

        private async void UpdateSpentTimeList()
        {
            var records = await _provider.GetTimeEntries(SpentOnDate, _selectedUser.Id);
            var projects = await LoadProjects();
            var issues = new List<Issue>();
            SpentTimeRecords.Clear();
            foreach (var timeEntry in records)
            {
                if (IssuesLookup.ContainsKey(timeEntry.ProjectId))
                {
                    issues = IssuesLookup[timeEntry.ProjectId];
                }
                else
                {
                    issues = await _provider.GetIssuesListForProjectAsync(timeEntry.ProjectId);
                    IssuesLookup.Add(timeEntry.ProjectId, issues);
                }
                SpentTimeRecords.Add(new SpentTimeRecordViewModel
                {
                    ProjectName = projects.SingleOrDefault(p => p.Id == timeEntry.ProjectId)?.Name,
                    TaskName = issues.SingleOrDefault(p => p.Id == timeEntry.IssueId)?.Name,
                    SpentTime = timeEntry.SpentTime
                });
            }
        }

        #region Full Properties

        private User _selectedUser;

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                UpdateSpentTimeList();
            }
        }
        
        private DateTime _spentOnDate;

        public DateTime SpentOnDate
        {
            get { return _spentOnDate; }
            set
            {
                _spentOnDate = value; 
                OnPropertyChanged();
                if (SelectedUser != null)
                    UpdateSpentTimeList();
            }
        }


        #endregion


    }
}