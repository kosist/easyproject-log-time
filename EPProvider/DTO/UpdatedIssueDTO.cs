using System.Xml.Serialization;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [SerializeAs(Name = "issue")]
    public class UpdatedIssueDTO
    {
        [SerializeAs(Name = "status_id")]
        public int StatusId { get; set; }

        [SerializeAs(Name = "done_ratio")]
        public int DoneRatio { get; set; }
    }
}