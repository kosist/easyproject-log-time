using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public interface ILoginViewModel
    {
        Task TestLogin();
        Task SaveCredentials();
    }
}
