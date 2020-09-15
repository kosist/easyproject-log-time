using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "user")]
    public class UserDTO
    {
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "id")]
        public int Id { get; set; }
    }
}