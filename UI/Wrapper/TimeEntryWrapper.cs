﻿using System;
using System.Globalization;
using System.Collections.Generic;
using BaseLayer.Model;
using Support.Helper;
using UI.ViewModel;

namespace UI.Wrapper
{
    public class TimeEntryWrapper : ModelWrapper<TimeEntryItemViewModel>
    {
        public TimeEntryWrapper(TimeEntryItemViewModel model) : base(model)
        {
            SelectedProject = null;
            SelectedIssue = null;
            SelectedUser = null;
            Description = "";
            SpentTime = "";
        }

        public int Id
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public Project SelectedProject
        {
            get => GetValue<Project>();
            set => SetValue(value);
        }

        public Issue SelectedIssue
        {
            get => GetValue<Issue>();
            set => SetValue(value);
        }

        public User SelectedUser
        {
            get => GetValue<User>();
            set => SetValue(value);
        }

        public string SpentTime
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Description
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public DateTime SpentOnDate
        {
            get => GetValue<DateTime>();
            set => SetValue(value);
        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(SelectedProject):
                    if ((SelectedProject == null) || (SelectedProject.Id == 0))
                    {
                        yield return "Project is not selected";
                    }
                    break;
                case nameof(SelectedIssue):
                    if (SelectedIssue == null)
                    {
                        yield return "Task is not selected";
                    }
                    break;
                case nameof(Description):
                    if (String.IsNullOrEmpty(Description) || String.IsNullOrWhiteSpace(Description))
                    {
                        yield return "Log description is empty";
                    }
                    break;
                case nameof(SpentTime):
                    if (String.IsNullOrEmpty(SpentTime) || String.IsNullOrWhiteSpace(SpentTime))
                    {
                        yield return "Time entry is empty";
                    }
                    else if (!ValidateSpentTime(SpentTime))
                    {
                        var culture = CultureInfo.CurrentCulture;
                        yield return $"Time entry has wrong format. Examples of valid formats: 1{culture.NumberFormat.NumberDecimalSeparator}5, 2:30, 05:45";
                    }
                    break;
            }
        }

        private bool ValidateSpentTime(string spentTime)
        {
            bool timeFormatRegexValid = SpentTimeValidation.CheckTimeFormatPattern(spentTime);
            bool doubleFormatRegexValid = SpentTimeValidation.CheckDoubleFormatPattern(spentTime);
            bool foundGroupSeparator = SpentTimeValidation.CheckGroupSeparator(spentTime);
            var validFlag = !foundGroupSeparator && (doubleFormatRegexValid || timeFormatRegexValid);
            return validFlag;
        }
    }
}