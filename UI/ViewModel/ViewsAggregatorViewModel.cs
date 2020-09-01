using Prism.Events;

namespace UI.ViewModel
{
    public class ViewsAggregatorViewModel : IViewsAggregatorViewModel
    {
        public ITimeEntryViewModel TimeEntryViewModel { get; }
        public ILoginViewModel LoginViewModel { get; }
        public ITabViewModel TabViewModel { get; }
        public ViewsAggregatorViewModel(ITimeEntryViewModel timeEntryViewModel, ILoginViewModel loginViewModel, ITabViewModel tabViewModel)
        {
            TimeEntryViewModel = timeEntryViewModel;
            LoginViewModel = loginViewModel;
            TabViewModel = tabViewModel;
        }
    }
}