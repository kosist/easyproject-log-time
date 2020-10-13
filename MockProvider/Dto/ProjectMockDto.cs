using System.Collections.Generic;

namespace MockProvider
{
    public class ProjectMockDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IssueMockDto> Issues { get; set; }
    }
}
