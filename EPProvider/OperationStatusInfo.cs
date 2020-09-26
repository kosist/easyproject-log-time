namespace EPProvider
{
    /// <summary>
    /// This class is used to hold information about login status - OK/NOK, and error/failure message
    /// </summary>
    public class OperationStatusInfo
    {
        public bool OperationStatus { get; set; }
        public string OperationMessage { get; set; }
    }
}