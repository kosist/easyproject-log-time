using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs (Name ="status")]
    public class IssueStatusDTO
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
    }
}