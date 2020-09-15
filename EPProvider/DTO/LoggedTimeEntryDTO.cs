using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "time_entry")]
    public class LoggedTimeEntryDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
        public ProjectDTO Project { get; set; }

        public IssueDTO Issue { get; set; }

        [DeserializeAs(Name = "hours")]
        public string SpentTime { get; set; }

        [DeserializeAs(Name = "comments")]
        public string Description { get; set; }

        [DeserializeAs(Name = "spent_on")]
        public string SpentOnDate { get; set; }

        public UserDTO User { get; set; }
    }

}