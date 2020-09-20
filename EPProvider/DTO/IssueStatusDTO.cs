using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [DeserializeAs (Name ="status")]
    [SerializeAs (Name ="status")]
    public class IssueStatusDTO
    {
        [DeserializeAs(Name = "id")]
        [SerializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        [SerializeAs(Name = "name")]
        public string Name { get; set; }
    }
}