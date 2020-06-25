using System.Globalization;
using System.Windows.Controls;
using BaseLayer.Model;

namespace UI.Rule
{
    public class ProjectRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Project project = (Project)value;
            return new ValidationResult(project != null, "Project is not selected!");
        }
    }
}