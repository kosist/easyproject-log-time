using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BaseLayer.Model;
using EPProvider;

namespace MockProvider
{
    public class MockEpProvider : IEPProvider
    {
        private IMapper _mapper;

        public List<ProjectMockDto> ProjectsList { get; set; }
        public List<IssueMockDto> IssuesList { get; set; }
        public List<TimeEntryMockDto> TimeEntries { get; set; }
        public List<UserMockDto> Users { get; set; }

        private void BuildProjectsList()
        {
            List<IssueMockDto> projectAIssues = new List<IssueMockDto>();
            List<IssueMockDto> projectBIssues = new List<IssueMockDto>();

            projectAIssues.Add(new IssueMockDto
            {
                Id = 1,
                Name = "DIO Control Module",
                DoneRatio = 10,
                Status = new IssueStatusMockDto
                {
                    Id = 3,
                    Name = "In Progress"
                },
                Parent = new IssueParentMockDto
                {
                    Id = 0,
                },
            });
            projectAIssues.Add(new IssueMockDto
            {
                Id = 2,
                Name = "PS Control",
                DoneRatio = 20,
                Status = new IssueStatusMockDto
                {
                    Id = 2,
                    Name = "New"
                },
                Parent = new IssueParentMockDto
                {
                    Id = 0,
                },
            });
            projectAIssues.Add(new IssueMockDto
            {
                Id = 0,
                Name = "Implementation",
                DoneRatio = 0,
                Status = new IssueStatusMockDto
                {
                    Id = 3,
                    Name = "In Progress"
                },
                Parent = new IssueParentMockDto
                {
                    Id = -1,
                },
            });

            IssuesList.AddRange(projectAIssues);

            ProjectsList.Add(new ProjectMockDto
            {
                Id = 1,
                Name = "2009 AKT Motor CU",
                Issues = projectAIssues,
            });
        }

        private void GetUsers()
        {
            Users.Clear();
            Users.Add(new UserMockDto
            {
                Id = 0,
                Name = "Ivan Yakushchenko",
            });
            Users.Add(new UserMockDto
            {
                Id = 1,
                Name = "John Smith",
            });
            Users.Add(new UserMockDto
            {
                Id = 2,
                Name = "Santa Claus",
            });
        }

        private void ListTimeEntries()
        {
            TimeEntries.Add(new TimeEntryMockDto
            {
                Id = 1,
                ProjectId = 1,
                IssueId = 1,
                UserId = 0,
                SpentOnDate = DateTime.Now,
                SpentTime = "1.5",
                Description = "Implemented the plugin"
            });
            TimeEntries.Add(new TimeEntryMockDto
            {
                Id = 2,
                ProjectId = 1,
                IssueId = 2,
                UserId = 0,
                SpentOnDate = DateTime.Now,
                SpentTime = "6.5",
                Description = "Tested the UI"
            });
        }

        public MockEpProvider(IMapper mapper)
        {
            _mapper = mapper;
            ProjectsList = new List<ProjectMockDto>();
            IssuesList = new List<IssueMockDto>();
            TimeEntries = new List<TimeEntryMockDto>();
            Users = new List<UserMockDto>();
            BuildProjectsList();
            GetUsers();
            ListTimeEntries();
        }
        public async Task<(OperationStatusInfo status, List<Project> projectsList)> GetProjectsListAsync()
        {
            var projects = ProjectsList.Select(_mapper.Map<ProjectMockDto, Project>).ToList();
            return (new OperationStatusInfo
            {
                OperationMessage = "Login is valid",
                OperationStatus = true
            }, projects);
        }

        public async Task<List<Issue>> GetIssuesListForProjectAsync(int projectId)
        {
            var issues = ProjectsList.Single(proj => proj.Id == projectId).Issues;
            return issues.Select(_mapper.Map<IssueMockDto, Issue>).ToList();
        }

        public async Task<OperationStatusInfo> AddTimeEntry(TimeEntry timeEntryData)
        {
            var newId = TimeEntries.Max(id => id.Id) + 1;
            var timeEntryId = timeEntryData.Id;
            if (timeEntryId == 0)
                timeEntryId = newId;
            TimeEntries.Add(new TimeEntryMockDto
            {
                Id = timeEntryId,
                ProjectId = timeEntryData.ProjectId,
                IssueId = timeEntryData.IssueId,
                UserId = timeEntryData.UserId,
                SpentOnDate = timeEntryData.SpentOnDate,
                SpentTime = timeEntryData.SpentTime,
                Description = timeEntryData.Description,
            });
            return new OperationStatusInfo
            {
                OperationStatus = true,
            };
        }

        public async Task<OperationStatusInfo> UpdateTimeEntry(TimeEntry timeEntryData)
        {
            var entry = TimeEntries.Single(e => e.Id == timeEntryData.Id);
            entry.ProjectId = timeEntryData.ProjectId;
            entry.IssueId = timeEntryData.IssueId;
            entry.UserId = timeEntryData.UserId;
            entry.SpentOnDate = timeEntryData.SpentOnDate;
            entry.SpentTime = timeEntryData.SpentTime;
            entry.Description = timeEntryData.Description;
            return new OperationStatusInfo
            {
                OperationStatus = true,
            };
        }

        public async Task<OperationStatusInfo> CredentialsValid()
        {
            return new OperationStatusInfo
            {
                OperationMessage = "Login is valid",
                OperationStatus = true
            };
        }

        public async Task<List<TimeEntry>> GetTimeEntries(DateTime date, int userId)
        {
            return TimeEntries.Select(_mapper.Map<TimeEntryMockDto, TimeEntry>)
                .Where(entry => entry.UserId == userId).Where(entry => entry.SpentOnDate.Date == date.Date).ToList();
        }

        public async Task<(OperationStatusInfo status, int userId)> GetCurrentUserId()
        {
            return (new OperationStatusInfo
            {
                OperationStatus = true,
            }, 0);
        }

        public async Task<List<User>> GetProjectUsersListAsync(int projectId)
        {
            return Users.Select(_mapper.Map<UserMockDto, User>).ToList();
        }

        public async Task<OperationStatusInfo> UpdateIssueStatus(UpdatedIssue issue)
        {
            var issueToUpdate = IssuesList.Single(e => e.Id == issue.Id);
            var updatedIssue = _mapper.Map<UpdatedIssue, UpdatedIssueMockDto>(issue);
            issueToUpdate.Status = updatedIssue.Status;
            issueToUpdate.DoneRatio = updatedIssue.DoneRatio;
            return new OperationStatusInfo
            {
                OperationStatus = true,
            };
        }

        public async Task<List<User>> GetUsersList()
        {
            return Users.Select(_mapper.Map<UserMockDto, User>).ToList();
        }
    }
}