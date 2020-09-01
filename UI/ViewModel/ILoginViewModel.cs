using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EPProvider;

namespace UI.ViewModel
{
    public interface ILoginViewModel
    {
        Task TestLogin();
        Task SaveCredentials();
        Credentials LoadCredentials();
    }
}
