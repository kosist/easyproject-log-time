using System;
using BaseLayer.Model;
using EPProvider;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using UI.Wrapper;

namespace UI.ViewModel
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private ICredentialsProvider _credentialsProvider;
        private IEventAggregator _eventAggregator;

        public LoginViewModel(ICredentialsProvider credentialsProvider, IEventAggregator eventAggregator)
        {
            _credentialsProvider = credentialsProvider;
            _eventAggregator = eventAggregator;
            Credentials = new CredentialsWrapper(new CredentialsItemViewModel());
            GetCredentials(LoadCredentials());
        }
        
        public Task SaveCredentials()
        {
            throw new NotImplementedException();
        }

        public Credentials LoadCredentials()
        {
            var credentials = _credentialsProvider.LoadCredentials();
            return credentials;
        }

        private void GetCredentials(Credentials credentials)
        {
            Credentials.UserName = credentials.UserName;
            Credentials.UserPassword = credentials.Password;
        }

        public Task TestLogin()
        {
            throw new NotImplementedException();
        }

        #region Full Properties

        private CredentialsWrapper credentials;

        public CredentialsWrapper Credentials
        {
            get { return credentials; }
            set
            {
                credentials = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }
}
