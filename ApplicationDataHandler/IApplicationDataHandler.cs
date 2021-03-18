using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationDataHandler
{
    public interface IApplicationDataHandler
    {
        Task<bool> GetStoreCredentialsFlag();
        Task SetStoreCredentialsFlag(bool storeCredentialsFlag);
        Task UpdateUsersList(List<UserMetaData> users);
        Task<UserMetaData> SetActiveUser(string login);
        Task<int> GetActiveUserId();
        Task<List<int>> GetUserIds();
    }
}
