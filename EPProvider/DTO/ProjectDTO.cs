using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "project")]
    public class ProjectDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
    }
}