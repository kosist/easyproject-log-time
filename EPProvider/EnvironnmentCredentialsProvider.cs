using System;

namespace EPProvider
{
    public class EnvironnmentCredentialsProvider : ICredentialsProvider
    {
        public string LoadAPIKey()
        {
            string easyProjectAPIKey = Environment.GetEnvironmentVariable("EasyProjectAPIKey", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(easyProjectAPIKey))
            {
                throw new ArgumentNullException(@"EasyProject API Key is empty. Please, set 'EasyProjectAPIKey' system variable value!");
            }
            return easyProjectAPIKey;
        }

        public Credentials LoadCredentials()
        {
            string easyProjectUserName = Environment.GetEnvironmentVariable("EasyProjectUserName", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(easyProjectUserName) || string.IsNullOrWhiteSpace(easyProjectUserName))
            {
                //throw new ArgumentNullException(@"EasyProjectUserName is empty. Please, set 'EasyProjectUserName' system variable value!");
                easyProjectUserName = "";
            }
            string easyProjectPassword = Environment.GetEnvironmentVariable("EasyProjectPassword", EnvironmentVariableTarget.User);
            if (string.IsNullOrEmpty(easyProjectPassword) || string.IsNullOrWhiteSpace(easyProjectPassword))
            {
                //throw new ArgumentNullException(@"EasyProjectPassword is empty. Please, set 'EasyProjectPassword' system variable value!");
                easyProjectPassword = "";
            }
            return new Credentials(easyProjectUserName, easyProjectPassword);
        }

        public void SaveCredentials(Credentials credentials)
        {
            if (credentials == null)
            {
                throw new ArgumentNullException();
            }

            if (string.IsNullOrEmpty(credentials.UserName) || string.IsNullOrWhiteSpace(credentials.UserName))
            {
                throw new ArgumentNullException("Username can not be null, empty or space");
            }

            if (string.IsNullOrEmpty(credentials.Password) || string.IsNullOrWhiteSpace(credentials.Password))
            {
                throw new ArgumentNullException("Password can not be null, empty or space");
            }

            Environment.SetEnvironmentVariable("EasyProjectUserName", credentials.UserName, EnvironmentVariableTarget.User);
            Environment.SetEnvironmentVariable("EasyProjectPassword", credentials.Password, EnvironmentVariableTarget.User);
        }
    }
}