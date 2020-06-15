namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ITimeEntryViewModel TimeEntryViewModel { get; }

        public MainViewModel(ITimeEntryViewModel timeEntryViewModel)
        {
            TimeEntryViewModel = timeEntryViewModel;
        }
    }
}