﻿using RestSharp.Serializers;

namespace EPProvider.DTO
{
    [SerializeAs(Name = "time_entry")]
    public class TimeEntryDTO
    {
        [SerializeAs(Name = "id")]
        public int Id { get; set; }
        [SerializeAs(Name = "project_id")]
        public int ProjectId { get; set; }

        [SerializeAs(Name = "issue_id")]
        public int IssueId { get; set; }

        [SerializeAs(Name = "user_id")]
        public int UserId { get; set; }

        [SerializeAs(Name ="hours")]
        public string SpentTime { get; set; }

        [SerializeAs(Name ="comments")]
        public string Description { get; set; }

        [SerializeAs(Name = "spent_on")]
        public string SpentOnDate { get; set; }
    }
}
