using System.Collections.ObjectModel;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public class TaskStatusesViewModel : ViewModelBase
    {
        private ObservableCollection<IssueStatus> _statuses;

        public ObservableCollection<IssueStatus> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }

        private IssueStatus _taskStatus;

        public IssueStatus TaskStatus
        {
            get { return _taskStatus; }
            set
            {
                _taskStatus = value;
                OnPropertyChanged();
            }
        }

        public TaskStatusesViewModel()
        {
            Statuses = new ObservableCollection<IssueStatus>();
            Statuses.Add(new IssueStatus
            {
                Id = 2,
                Name = "New"
            });
            Statuses.Add(new IssueStatus
            {
                Id = 3,
                Name = "In Progress"
            });
            Statuses.Add(new IssueStatus
            {
                Id = 4,
                Name = "Done"
            });
            Statuses.Add(new IssueStatus
            {
                Id = 11,
                Name = "Blocked"
            });
        }
    }
}