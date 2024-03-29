﻿using RestSharp.Deserializers;
using System.Collections.Generic;

namespace EPProvider.DTO
{
    [DeserializeAs(Name = "project")]
    public class ProjectDTO
    {
        [DeserializeAs(Name = "id")]
        public int Id { get; set; }

        [DeserializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "custom_fields")]
        public List<CustomFieldDTO> CustomFields { get; set; }
    }
}