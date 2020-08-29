using UI.DataModel;

namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public ITimeEntryViewModel TimeEntryViewModel { get; }

        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.TimeLogger;

        public MainViewModel(ITimeEntryViewModel timeEntryViewModel)
        {
            TimeEntryViewModel = timeEntryViewModel;
        }
    }
}