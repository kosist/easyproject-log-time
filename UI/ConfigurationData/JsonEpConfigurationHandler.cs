using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace UI.ConfigurationData
{
    public class JsonEpConfigurationHandler : IEpConfigurationParameters
    {
        public IConfiguration Configuration { get; set; }
        public EpConfigData EpConfigData { get; set; }
        public JsonEpConfigurationHandler()
        {
            EpConfigData = new EpConfigData();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public Task<bool> GetRememberCredentialsFlag()
        {
            Configuration.GetSection(nameof(EpConfigData))
                .Bind(EpConfigData);
            return Task.FromResult(EpConfigData.RememberCredentials);

        }

        public Task SaveRememberCredentialsFlag(bool rememberCredentials)
        {
            throw new System.NotImplementedException();
        }
    }
}