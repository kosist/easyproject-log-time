using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "issue")]
    public class IssueDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }

        public ParentDTO Parent { get; set; }

        [DeserializeAs(Name = "subject")]
        public string Name { get; set; }

        public IssueStatusDTO Status { get; set; }

        [DeserializeAs(Name = "done_ratio")]
        public int DoneRatio { get; set; }
    }
}