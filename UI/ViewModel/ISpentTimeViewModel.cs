using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLayer.Model;

namespace UI.ViewModel
{
    public interface ISpentTimeViewModel
    {
        Task<List<TimeEntry>> LoadTimeEntries(DateTime date);
        Task<List<User>> LoadUsersList();
    }
}