using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [DeserializeAs (Name ="status")]
    public class IssueStatusDTO
    {
        [DeserializeAs(Name = "id")]
        [SerializeAs(Name = "status_id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
    }
}