using System.Collections.Generic;
using UI.ViewModel;

namespace UI.Wrapper
{
    public class CredentialsWrapper : ModelWrapper<CredentialsItemViewModel>
    {
        #region Public Properties
        public string UserName
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string UserPassword
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool Valid
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }
        #endregion

        public CredentialsWrapper(CredentialsItemViewModel model) : base(model)
        {

        }

        protected override IEnumerable<string> ValidateProperty(string propertyName)
        {
            switch (propertyName)
            {
                case nameof(UserName):
                    if (string.IsNullOrEmpty(UserName) || string.IsNullOrWhiteSpace(UserName))
                    {
                        yield return "User name is empty!";
                    }
                    break;
                case nameof(UserPassword):
                    if (string.IsNullOrEmpty(UserPassword) || string.IsNullOrWhiteSpace(UserPassword))
                    {
                        yield return "Password is empty!";
                    }
                    break;
            }
        }
    }
}