using System.Collections.Generic;

namespace MockProvider.Dto
{
    public class ProjectMockDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IssueStatusMockDto> Issues { get; set; }
    }
}
