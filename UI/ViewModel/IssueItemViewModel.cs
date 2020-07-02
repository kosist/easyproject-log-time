using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public class IssueItemViewModel : ViewModelBase
    {
        public ObservableCollection<IssueItemViewModel> Children = new ObservableCollection<IssueItemViewModel>();
        public IssueItemViewModel Parent { get; set; }
        public Issue AssociatedObject { get; set; }
        public string Name { get; set; }

        public IssueItemViewModel()
        {
            Children = new ObservableCollection<IssueItemViewModel>();
        }
    }
}