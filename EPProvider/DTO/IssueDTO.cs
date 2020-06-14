using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    public class IssueDTO
    {
        [DeserializeAs(Name = "issue")]
        public class Issue
        {
            [DeserializeAs(Name = "id")]
            public int Id { get; set; }

            public ParentDTO Parent { get; set; }

            [DeserializeAs(Name = "subject")]
            public string Name { get; set; }
        }
    }
}