using System.Threading.Tasks;

namespace UI.ConfigurationData
{
    public interface IEpConfigurationParameters
    {
        Task<bool> GetRememberCredentialsFlag();
        Task SaveRememberCredentialsFlag(bool rememberCredentials);
    }
}