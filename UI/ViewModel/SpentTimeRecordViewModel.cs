using BaseLayer.Model;

namespace UI.ViewModel
{
    public class SpentTimeRecordViewModel
    {
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public TimeEntry TimeEntry { get; set; }
    }
}