using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaseLayer.Model;
using EPProvider;

namespace UI.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase, ITimeEntryViewModel
    {
        private IEPProvider _provider;

        public ObservableCollection<Project> Projects { get; set; }

        public TimeEntryViewModel(IEPProvider provider)
        {
            _provider = provider;
            Projects = new ObservableCollection<Project>();
            LoadProjects();
        }

        public void LoadProjects()
        {
            Projects.Clear();
            var projects = _provider.GetProjectsList();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }
        }
    }
}