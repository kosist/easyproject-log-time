using UI.DataModel;

namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ITimeEntryViewModel TimeEntryViewModel { get; }
        public ILoginViewModel LoginViewModel { get; }
        public MainViewModel(ITimeEntryViewModel timeEntryViewModel, ILoginViewModel loginViewModel)
        {
            TimeEntryViewModel = timeEntryViewModel;
            LoginViewModel = loginViewModel;
        }
    }
}