﻿using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [DeserializeAs (Name ="status")]
    [SerializeAs (Name ="status")]
    public class IssueStatusDTO
    {
        [DeserializeAs(Name = "id")]
        [SerializeAs(Name = "id")]
        public string Id { get; set; }

        [DeserializeAs(Name = "name")]
        [SerializeAs(Name = "name")]
        public string Name { get; set; }

        [DeserializeAs(Name = "done_ratio")]
        [SerializeAs(Name = "done_ratio")]
        public int DoneRatio { get; set; }
    }
}