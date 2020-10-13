namespace MockProvider
{
    public class IssueMockDto
    {
        public int Id { get; set; }

        public IssueParentMockDto Parent { get; set; }

        public string Name { get; set; }

        public IssueStatusMockDto Status { get; set; }

        public int DoneRatio { get; set; }
    }
}
