namespace BaseLayer.Model
{
    public class UpdatedIssue
    {
        public int Id { get; set; }
        public IssueStatus Status { get; set; }
        public int DoneRatio { get; set; }
    }
}