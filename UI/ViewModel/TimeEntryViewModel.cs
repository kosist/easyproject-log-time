using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using BaseLayer.Model;
using EPProvider;
using Prism.Commands;
using Prism.Events;
using UI.Event;
using UI.Wrapper;

namespace UI.ViewModel
{
    public class TimeEntryViewModel : ViewModelBase, ITimeEntryViewModel
    {
        private IEPProvider _provider;
        private IEventAggregator _eventAggregator;

        public ObservableCollection<Project> Projects { get; private set; }
        public ObservableCollection<Issue> Issues { get; private set; }
        public ObservableCollection<IssueItemViewModel> Nodes { get; private set; }
        public ObservableCollection<IssueItemViewModel> Tasks { get; private set; }
        public DateTime SpentOnDate { get; set; }
        public ICommand SaveCommand { get; }

        public TimeEntryViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
            Projects = new ObservableCollection<Project>();
            Issues = new ObservableCollection<Issue>();
            Nodes = new ObservableCollection<IssueItemViewModel>();
            Tasks = new ObservableCollection<IssueItemViewModel>();
            SpentOnDate = DateTime.Today;
            DisplayProjectsAsync();

            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
        }

        private void OnSaveExecute()
        {
            var timeEntry = new TimeEntry
            {
                ProjectId = TimeEntry.SelectedProject.Id,
                IssueId = TimeEntry.SelectedIssue.Id,
                SpentOnDate = TimeEntry.SpentOnDate,
                Description = TimeEntry.Description,
                SpentTime = TimeEntry.SpentTime,
            };
            //var result = _provider.AddTimeEntry(timeEntry);
            //if (!result)
            //    throw new Exception("Post method executed with error!");

            TimeEntry = new TimeEntryWrapper(new TimeEntryItemViewModel());
            TimeEntry.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TimeEntry.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedProject")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";
                    DisplayIssuesList(TimeEntry.SelectedProject.Id);
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnSaveCanExecute()
        {
            return TimeEntry != null && !TimeEntry.HasErrors;
        }

        public async Task<List<Project>> LoadProjects()
        {
            return await _provider.GetProjectsListAsync();
        }

        private async Task DisplayProjectsAsync()
        {
            Projects.Clear();
            var projects = await LoadProjects();
            foreach (var project in projects)
            {
                Projects.Add(project);
            }

            TimeEntry = new TimeEntryWrapper(new TimeEntryItemViewModel());
            TimeEntry.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(TimeEntry.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == "SelectedProject")
                {
                    TimeEntry.SpentTime = "";
                    TimeEntry.Description = "";
                    DisplayIssuesList(TimeEntry.SelectedProject.Id);
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        public async Task<List<Issue>> LoadIssues(int projectId)
        {
            return await _provider.GetIssuesListForProjectAsync(projectId);
        }

        private async Task DisplayIssuesList(int projectId)
        {
            Issues.Clear();
            var issues = await LoadIssues(projectId);
            foreach (var issue in issues)
            {
                Issues.Add(issue);
            }
            BuildTreeAndGetRoots(Issues);
        }

        #region fullProperties

        private TimeEntryWrapper _timeEntry;

        public TimeEntryWrapper TimeEntry
        {
            get { return _timeEntry; }
            set
            {
                _timeEntry = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public class Node
        {
            public List<Node> Children = new List<Node>();
            public Node Parent { get; set; }
            public Issue AssociatedObject { get; set; }
            public string Name { get; set; }
        }

        public void BuildTreeAndGetRoots(ObservableCollection<Issue> actualObjects)
        {
            Nodes.Clear();
            Dictionary<int, IssueItemViewModel> lookup = new Dictionary<int, IssueItemViewModel>();

            foreach (var issue in actualObjects)
            {
                lookup.Add(issue.Id, new IssueItemViewModel
                {
                    AssociatedObject = issue,
                    Name = issue.Name,
                    SelectedName = issue.Name,
                    Level = 0,
                });
            }

            //actualObjects.ForEach(x => lookup.Add(x.Id, new Node { AssociatedObject = x }));
            foreach (var item in lookup.Values)
            {
                IssueItemViewModel proposedParent;
                if (lookup.TryGetValue(item.AssociatedObject.ParentId, out proposedParent))
                {
                    item.Parent = proposedParent;
                    proposedParent.Children.Add(item);
                }
            }
            //return lookup.Values.Where(x => x.Parent == null);
            foreach (var node in lookup)
            {
                if (node.Value.Parent == null)
                {
                    Nodes.Add(node.Value);
                }
            }
            Tasks.Clear();
            UpdateLevel(Nodes);
            IndentNames(Nodes);
        }

        private void IndentNames(ObservableCollection<IssueItemViewModel> nodes)
        {
            
            foreach (var node in nodes)
            {
                var item = new IssueItemViewModel();
                item.AssociatedObject = node.AssociatedObject;
                item.Parent = node.Parent;
                item.Level = node.Level;
                item.Name = node.Name;
                item.SelectedName = node.SelectedName;
                foreach (var child in node.Children)
                {
                    item.Children.Add(child);
                }
                for (int i = 0; i < item.Level; i++)
                {
                    item.Name = "   " + item.Name;
                }
                Tasks.Add(item);
                IndentNames(new ObservableCollection<IssueItemViewModel>(node.Children));
            }
        }

        private void UpdateLevel(ObservableCollection<IssueItemViewModel> items)
        {
            foreach (var item in items)
            {
                if (item.Parent != null)
                {
                    if (item.Level != item.Parent.Level + 1)
                    {
                        item.Level = item.Parent.Level + 1;
                    }
                }
                else
                {
                    item.Level++;
                }
                UpdateLevel(item.Children);
            }
        }
    }
}