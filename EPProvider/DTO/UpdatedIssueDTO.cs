using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [SerializeAs(Name = "issue")]
    public class UpdatedIssueDTO
    {
        [SerializeAs(Name = "id")]
        public int Id { get; set; }
        public IssueStatusDTO Status { get; set; }

        [SerializeAs(Name = "done_ratio")]
        public int DoneRatio { get; set; }
    }
}