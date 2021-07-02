using BaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Support
{
    public class ProcessProjects
    {
        public static async Task<List<Project>> FilterProjects(List<Project> projects, List<string> types)
        {
            var filteredProjects = new List<Project>();
            foreach (var project in projects)
            {
                var filtered = project.CustomFields.Where(c => types.Any(t => (t == c.Value) & (c.Name == "Type")));
                if (filtered.Count() != 0)
                    filteredProjects.Add(project);
            }
            return filteredProjects;
        }
    }
}
