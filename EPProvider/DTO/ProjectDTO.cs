using RestSharp.Deserializers;

namespace EPProvider.DTO
{
    public class ProjectDTO
    {
        [DeserializeAs(Name = "project")]
        public class Project
        {
            [DeserializeAs(Name = "id")]
            public int Id { get; set; }

            [DeserializeAs(Name = "name")]
            public string Name { get; set; }
        }
    }
}