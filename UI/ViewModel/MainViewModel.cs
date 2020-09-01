using UI.DataModel;

namespace UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        public IViewsAggregatorViewModel ViewsAggregatorViewModel { get; }
        public MainViewModel(IViewsAggregatorViewModel viewsAggregatorViewModel)
        {
            ViewsAggregatorViewModel = viewsAggregatorViewModel;
        }
    }
}