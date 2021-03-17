using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDataHandler
{
    public class JsonApplicationDataHandler : IApplicationDataHandler
    {
        public int GetActiveUserId()
        {
            throw new NotImplementedException();
        }

        public bool GetStoreCredentialsFlag()
        {
            throw new NotImplementedException();
        }

        public List<int> GetUserIds()
        {
            throw new NotImplementedException();
        }

        public UserMetaData SetActiveUser(string login)
        {
            throw new NotImplementedException();
        }

        public void SetStoreCredentialsFlag(bool storeCredentialsFlag)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsersList(List<UserMetaData> users)
        {
            throw new NotImplementedException();
        }
    }
}
