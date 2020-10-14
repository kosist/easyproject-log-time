using Prism.Events;
using UI.Event;

namespace UI.ViewModel
{
    public class TabViewModel : ViewModelBase, ITabViewModel
    {
        private IEventAggregator _eventAggregator;

        public TabViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<LoginSuccessEvent>().Subscribe(OnLoginExecute);
            _eventAggregator.GetEvent<SelectLogHoursTabEvent>().Subscribe(OnSelectLogHoursTabEvent);

            LoggerPageActive = false;
            LogHoursTabSelected = false;
            LoginTabEnabled = true;
        }

        private void OnSelectLogHoursTabEvent()
        {
            LogHoursTabSelected = true;
        }

        private void OnLoginExecute(bool status)
        {
            LoggerPageActive = status;
            LogHoursTabSelected = status;
            LoginTabEnabled = !status;
        }

        #region Full Properties

        private bool loggerPageActive;

        public bool LoggerPageActive
        {
            get { return loggerPageActive; }
            set
            {
                loggerPageActive = value;
                OnPropertyChanged();
            }
        }

        private bool logHoursTabSelected;

        public bool LogHoursTabSelected
        {
            get { return logHoursTabSelected; }
            set
            {
                logHoursTabSelected = value;
                OnPropertyChanged();
            }
        }

        private bool loginTabEnabled;

        public bool LoginTabEnabled
        {
            get { return loginTabEnabled; }
            set
            {
                loginTabEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

    }
}