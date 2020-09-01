namespace EPProvider
{
    public class Credentials
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public Credentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}