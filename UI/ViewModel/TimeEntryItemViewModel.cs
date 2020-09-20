using BaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.ViewModel
{
    public class TimeEntryItemViewModel
    {
        public Project SelectedProject { get; set; }
        public Issue SelectedIssue { get; set; }

        public string SpentTime { get; set; }

        public string Description { get; set; }

        public DateTime SpentOnDate { get; set; }
        public User User { get; set; }

        public TimeEntryItemViewModel()
        {
            SpentOnDate = DateTime.Now;
        }

    }
}
