using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [SerializeAs(Name = "time_entry")]
    public class LoggedTimeEntryDTO
    {
        public ProjectDTO Project { get; set; }

        public IssueDTO Issue { get; set; }

        [SerializeAs(Name = "hours")]
        public string SpentTime { get; set; }

        [SerializeAs(Name = "comments")]
        public string Description { get; set; }

        [SerializeAs(Name = "spent_on")]
        public string SpentOnDate { get; set; }
    }
}