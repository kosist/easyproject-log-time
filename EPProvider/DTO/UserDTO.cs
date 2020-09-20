using System.Xml.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "user")]
    [SerializeAs(Name = "user")]
    public class UserDTO
    {
        [DeserializeAs(Name = "id")]
        [SerializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        [SerializeAs(Name = "name")]
        public string Name { get; set; }
    }
}