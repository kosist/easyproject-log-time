using UI.DataModel;

namespace UI.ViewModel
{
    public class StatusMessageViewModel : ViewModelBase, IStatusMessageString
    {
        #region Constructor
        public StatusMessageViewModel()
        {
            StatusMessage = "";
            StatusColor = StatusEnum.Normal;
        }        

        #endregion

        #region Public methods

        /// <summary>
        /// This method sets status text and its color. If text is empty, then color is white   
        /// </summary>
        /// <param name="message">Status message text</param>
        /// <param name="statusColor">Color of the status message</param>
        public void UpdateStatusMessage(string message, StatusEnum statusColor = StatusEnum.Normal)
        {
            if (string.IsNullOrEmpty(message))
            {
                StatusMessage = "";
                StatusColor = StatusEnum.Normal;
            }
            else
            {
                StatusMessage = message;
                StatusColor = statusColor;
            }
        }        

        #endregion
        
        #region Full Properties

        private string statusMessage;

        public string StatusMessage
        {
            get { return statusMessage; }
            set
            {
                statusMessage = value; 
                OnPropertyChanged();
            }
        }

        private StatusEnum statusColor;

        public StatusEnum StatusColor
        {
            get { return statusColor; }
            set
            {
                statusColor = value; 
                OnPropertyChanged();
            }
        }

        #endregion
    }
}