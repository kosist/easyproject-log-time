using System;

namespace MockProvider
{
    public class TimeEntryMockDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int IssueId { get; set; }
        public int UserId { get; set; }
        public string SpentTime { get; set; }
        public string Description { get; set; }
        public DateTime SpentOnDate { get; set; }
    }
}
