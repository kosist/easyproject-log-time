using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDataHandler
{
    public interface IApplicationDataHandler
    {
        bool GetStoreCredentialsFlag();
        void SetStoreCredentialsFlag(bool storeCredentialsFlag);
        void UpdateUsersList(List<UserMetaData> users);
        UserMetaData SetActiveUser(string login);
        int GetActiveUserId();
        List<int> GetUserIds();
    }
}
