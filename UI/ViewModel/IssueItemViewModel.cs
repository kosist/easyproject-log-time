using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public class IssueItemViewModel : ViewModelBase
    {
        public ObservableCollection<IssueItemViewModel> Children { get; private set; }
        public IssueItemViewModel Parent { get; set; }
        public Issue AssociatedObject { get; set; }
        public string Name { get; set; }
        public string SelectedName { get; set; }
        public int Level { get; set; }

        public IssueItemViewModel()
        {
            Children = new ObservableCollection<IssueItemViewModel>();
        }
    }
}