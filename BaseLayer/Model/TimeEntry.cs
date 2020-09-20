﻿using System;

namespace BaseLayer.Model
{
    public class TimeEntry
    {
        public int ProjectId { get; set; }
        public int IssueId { get; set; }
        public string SpentTime { get; set; }
        public string Description { get; set; }
        public DateTime SpentOnDate { get; set; }
        public User User { get; set; }
    }
}