using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "parent")]
    public class ParentDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }
}