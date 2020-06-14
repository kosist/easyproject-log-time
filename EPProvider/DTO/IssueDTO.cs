using RestSharp.Deserializers;

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
    }
}