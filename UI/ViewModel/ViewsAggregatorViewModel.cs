using Prism.Events;

namespace UI.ViewModel
{
    public class ViewsAggregatorViewModel : IViewsAggregatorViewModel
    {
        public ITimeEntryViewModel TimeEntryViewModel { get; }
        public ILoginViewModel LoginViewModel { get; }
        public ITabViewModel TabViewModel { get; }
        public ISpentTimeViewModel SpentTimeViewModel { get; }
        public ViewsAggregatorViewModel(ITimeEntryViewModel timeEntryViewModel, ILoginViewModel loginViewModel, ITabViewModel tabViewModel, ISpentTimeViewModel spentTimeViewModel)
        {
            TimeEntryViewModel = timeEntryViewModel;
            LoginViewModel = loginViewModel;
            TabViewModel = tabViewModel;
            SpentTimeViewModel = spentTimeViewModel;
        }
    }
}