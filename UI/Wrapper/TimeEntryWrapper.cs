using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using BaseLayer.Model;
using UI.ViewModel;

namespace UI.Wrapper
{
    public class TimeEntryWrapper : ModelWrapper<TimeEntryItemViewModel>
    {
        public TimeEntryWrapper(TimeEntryItemViewModel model) : base(model)
        {
            SelectedProject = null;
            SelectedIssue = null;
            SpentTime = "";
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
                    if (SelectedProject == null)
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
                case nameof(SpentTime):
                    if (String.IsNullOrEmpty(SpentTime) || String.IsNullOrWhiteSpace(SpentTime))
                    {
                        yield return "Time entry is empty";
                    }
                    else if (!ValidateSpentTime(SpentTime))
                    {
                        yield return "Time entry has wrong format. Examples of valid formats: 1.5, 2:30, 05:45";
                    }
                    break;
            }
        }

        private bool ValidateSpentTime(string spentTime)
        {
            var cultureInfo = CultureInfo.CurrentCulture;
            double result = 0.0;
            string text = spentTime;

            var timeFormatPattern = @"^(?:0?[0-9]|1[0-9]|2[0-9]):[0-5][0-9]$";
            Regex timeFormatRegex = new Regex(timeFormatPattern);
            bool timeFormatRegexValid = timeFormatRegex.IsMatch(text);

            var doubleFormatPattern = $@"^(\d+\{cultureInfo.NumberFormat.NumberDecimalSeparator}?\d{{0,2}}$)[\d.]{{0,2}}$";
            Regex doubleFormatRegex = new Regex(doubleFormatPattern);
            bool doubleFormatRegexValid = doubleFormatRegex.IsMatch(text);

            string groupSeparator = cultureInfo.NumberFormat.NumberGroupSeparator;
            bool canConvert = double.TryParse(text, out result);
            bool foundGroupSeparator = text.Contains(groupSeparator);
            var validFlag = !foundGroupSeparator && ((canConvert && doubleFormatRegexValid) || timeFormatRegexValid);
            return validFlag;
        }
    }
}