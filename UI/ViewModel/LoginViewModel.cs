using System;
using BaseLayer.Model;
using EPProvider;
using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Event;
using UI.Wrapper;

namespace UI.ViewModel
{
    public class LoginViewModel : ViewModelBase, ILoginViewModel
    {
        private ICredentialsProvider _credentialsProvider;
        private IEventAggregator _eventAggregator;
        private IEPProvider _provider;

        public ICommand LoginCommand { get; }

        public LoginViewModel(ICredentialsProvider credentialsProvider, IEventAggregator eventAggregator, IEPProvider provider)
        {
            _credentialsProvider = credentialsProvider;
            _eventAggregator = eventAggregator;
            _provider = provider;

            LoginCommand = new DelegateCommand(OnLoginExecute, OnLoginCanExecute);

            GetCredentials();

        }

        private void OnLoginExecute()
        {
           _credentialsProvider.SaveCredentials(new Credentials(Credentials.UserName, Credentials.UserPassword));
           GetCredentials();
           var status = _provider.CredentialsValid();
           if (status)
                _eventAggregator.GetEvent<LoginSuccessEvent>().Publish(true);
        }

        private bool OnLoginCanExecute()
        {
            return Credentials != null && !Credentials.HasErrors;
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

        private void GetCredentials()
        {
            var credentials = LoadCredentials();
            Credentials = new CredentialsWrapper(new CredentialsItemViewModel());
            Credentials.UserName = credentials.UserName;
            Credentials.UserPassword = credentials.Password;

            Credentials.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(Credentials.HasErrors))
                {
                    ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();
                }
            };
            ((DelegateCommand)LoginCommand).RaiseCanExecuteChanged();

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
