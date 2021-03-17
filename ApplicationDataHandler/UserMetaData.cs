using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationDataHandler
{
    public class UserMetaData
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; private set; }
        public string UserLogin { get; set; }
        public bool LoggedInUser { get; set; }

        public UserMetaData()
        {
            FullName = Name + " " + Surname;
        }
    }
}
