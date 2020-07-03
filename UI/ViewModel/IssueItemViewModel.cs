using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public class IssueItemViewModel : ViewModelBase
    {
        public List<IssueItemViewModel> Children { get; private set; }
        public IssueItemViewModel Parent { get; set; }
        public Issue AssociatedObject { get; set; }
        public string Name { get; set; }

        public IssueItemViewModel()
        {
            Children = new List<IssueItemViewModel>();
        }
    }
}