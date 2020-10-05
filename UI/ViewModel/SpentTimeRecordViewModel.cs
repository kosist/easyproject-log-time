using BaseLayer.Model;

namespace UI.ViewModel
{
    public class SpentTimeRecordViewModel
    {
        public User User { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string SpentTime { get; set; }
    }
}