using UI.DataModel;

namespace UI.ViewModel
{
    public interface IStatusMessageString
    {
        void UpdateStatusMessage(string message, StatusEnum statusColor = StatusEnum.Normal);
    }
}