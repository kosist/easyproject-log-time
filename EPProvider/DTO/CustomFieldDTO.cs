using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "custom_field")]
    public class CustomFieldDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "value")]
        public string Value { get; set; }
    }
}
