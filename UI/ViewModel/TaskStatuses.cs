using System.Collections.ObjectModel;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public class TaskStatuses : ObservableCollection<IssueStatus>
    {
        public TaskStatuses()
        {
            this.Add(new IssueStatus
            {
                Id = 2,
                Name = "New"
            });
            this.Add(new IssueStatus
            {
                Id = 3,
                Name = "In Progress"
            });
            this.Add(new IssueStatus
            {
                Id = 4,
                Name = "Done"
            });
            this.Add(new IssueStatus
            {
                Id = 11,
                Name = "Blocked"
            });
        }
    }
}