namespace UI.ViewModel
{
    public class TaskStatusViewModel : ViewModelBase, ITaskStatusViewModel
    {
        public DoneRatioList DoneRatioList { get; private set; }
        public TaskStatusesViewModel TaskStatuses { get; private set; }

        public TaskStatusViewModel()
        {
            DoneRatioList = new DoneRatioList();
            TaskStatuses = new TaskStatusesViewModel();
        }
    }
}