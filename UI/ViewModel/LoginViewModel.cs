using System;
using BaseLayer.Model;
using EPProvider;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;

namespace UI.ViewModel
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private IEPProvider _provider;
        private IEventAggregator _eventAggregator;

        public LoginViewModel(IEPProvider provider, IEventAggregator eventAggregator)
        {
            _provider = provider;
            _eventAggregator = eventAggregator;
        }
        
        public Task SaveCredentials()
        {
            throw new NotImplementedException();
        }

        public Task TestLogin()
        {
            throw new NotImplementedException();
        }
    }
}
