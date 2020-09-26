namespace EPProvider
{
    /// <summary>
    /// This class is used to hold information about login status - OK/NOK, and error/failure message
    /// </summary>
    public class LoginStatusInfo
    {
        public bool LoginStatus { get; set; }
        public string LoginMessage { get; set; }
    }
}