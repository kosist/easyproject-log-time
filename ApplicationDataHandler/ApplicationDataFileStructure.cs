using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDataHandler
{
    public class ApplicationDataFileStructure
    {
        public List<UserMetaData> UsersList { get; set; }
        public bool StoreCredentials { get; set; }
        public AppCredentials AppCredentials { get; set; }
    }
}
