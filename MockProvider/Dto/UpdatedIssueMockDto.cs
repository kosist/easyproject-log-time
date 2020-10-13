namespace MockProvider.Dto
{
    public class UpdatedIssueMockDto
    {
        public int Id { get; set; }
        public IssueStatusMockDto Status { get; set; }
        public int DoneRatio { get; set; }
    }
}
