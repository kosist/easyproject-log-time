namespace EPProvider
{
    public interface ICredentialsProvider
    {
        string LoadAPIKey();
        Credentials LoadCredentials();
        void SaveCredentials(Credentials credentials);
    }
}