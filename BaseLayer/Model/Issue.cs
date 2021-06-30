namespace BaseLayer.Model
{
    public class Issue
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public IssueStatus Status { get; set; }
        public int DoneRatio { get; set; }
        public double EstimatedHours { get; set; }
    }
}