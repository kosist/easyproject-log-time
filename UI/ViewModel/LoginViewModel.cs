﻿using System;
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
        #region Private Properties

        private ICredentialsProvider _credentialsProvider;
        private IEventAggregator _eventAggregator;
        private IEPProvider _provider;

        #endregion

        #region Public Properties

        public ICommand LoginCommand { get; }

        #endregion

        #region Constructor
        public LoginViewModel(ICredentialsProvider credentialsProvider, IEventAggregator eventAggregator, IEPProvider provider)
        {
            _credentialsProvider = credentialsProvider;
            _eventAggregator = eventAggregator;
            _provider = provider;

            LoginCommand = new DelegateCommand(OnLoginExecute, OnLoginCanExecute);

            GetCredentials();

        }

        #endregion

        #region Login Methods
        /// <summary>
        /// Save credentials, then read it back. After, checks its status and generates event LoginSuccessEvent
        /// </summary>
        private void OnLoginExecute()
        {
           _credentialsProvider.SaveCredentials(new Credentials(Credentials.UserName, Credentials.UserPassword));
           GetCredentials();
           var status = _provider.CredentialsValid();
           _eventAggregator.GetEvent<LoginSuccessEvent>().Publish(status);
        }

        /// <summary>
        /// Checks whether Login event could be executed
        /// </summary>
        /// <returns></returns>
        private bool OnLoginCanExecute()
        {
            return Credentials != null && !Credentials.HasErrors;
        }

        #endregion

        #region Save Methods
        /// <summary>
        /// Method should save credentials. Now there is no UI button for save action, thus method is not implemented
        /// </summary>
        /// <returns></returns>
        public Task SaveCredentials()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Load Methods

        /// <summary>
        /// Method loads and returns credentials
        /// </summary>
        /// <returns></returns>
        public Credentials LoadCredentials()
        {
            var credentials = _credentialsProvider.LoadCredentials();
            return credentials;
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Method gets credentials, and updates UI, waits for events
        /// </summary>
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

        #endregion

        #region Public Methods

        /// <summary>
        /// Method should test whether login is correct. Now login verification is done in another way, so this method is not implemented
        /// </summary>
        /// <returns></returns>
        public Task TestLogin()
        {
            throw new NotImplementedException();
        }

        #endregion
        
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