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
    }
}