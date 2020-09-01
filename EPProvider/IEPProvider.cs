﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;

namespace EPProvider
{
    public interface IEPProvider
    {
        Task<List<Project>> GetProjectsListAsync();
        Task<List<Issue>> GetIssuesListForProjectAsync(int projectId);
        bool AddTimeEntry(TimeEntry timeEntryData);
        bool CredentialsValid();
    }
}