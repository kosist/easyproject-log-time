using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BaseLayer.Model;
using EPProvider;
using Prism.Events;

namespace UI.ViewModel
{
    public class SpentTimeViewModel : ViewModelBase, ISpentTimeViewModel
    {
        private IEventAggregator _eventAggregator;
        private IEPProvider _provider;
        public ObservableCollection<SpentTimeRecordViewModel> SpentTimeRecords { get; private set; }
        public ObservableCollection<User> UsersList { get; private set; }

        public SpentTimeViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            SpentTimeRecords = new ObservableCollection<SpentTimeRecordViewModel>();
            UsersList = new ObservableCollection<User>();
            SpentOnDate = DateTime.Today;
            InitView();
        }



        public Task<List<TimeEntry>> LoadTimeEntries(DateTime date)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> LoadUsersList()
        {
            return await _provider.GetUsersList();
        }

        private async void InitView()
        {
            var users = await LoadUsersList();
            foreach (var user in users)
            {
                UsersList.Add(user);
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
            }
        }


        #endregion


    }
}