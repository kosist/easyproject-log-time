using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApplicationDataHandler
{
    public class JsonApplicationDataHandler : IApplicationDataHandler
    {
        private ApplicationDataFileStructure _appData { get; set; }
        private string _cfgFilePath { get; set; }

        public JsonApplicationDataHandler()
        {
            _cfgFilePath = "AppCfgData.json";            
        }

        private async void CheckCfgFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var cfgData = new ApplicationDataFileStructure();
                using FileStream writeStream = File.Create(filePath);
                await JsonSerializer.SerializeAsync(writeStream, cfgData);
            }
        }

        private async Task<ApplicationDataFileStructure> LoadCfgData(string filePath)
        {
            CheckCfgFile(_cfgFilePath);
            using FileStream openStream = File.OpenRead(filePath);
            _appData = await JsonSerializer.DeserializeAsync<ApplicationDataFileStructure>(openStream);
            return _appData;
        }

        private async void SaveCfgData(ApplicationDataFileStructure cfgData)
        {
            using FileStream writeStream = File.Create(_cfgFilePath);
            await JsonSerializer.SerializeAsync(writeStream, cfgData);
        }


        public Task<int> GetActiveUserId()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> GetStoreCredentialsFlag()
        {
            var cfgData = await LoadCfgData(_cfgFilePath);
            return cfgData.StoreCredentials;
        }
        public async Task SetStoreCredentialsFlag(bool storeCredentialsFlag)
        {
            var cfgData = await LoadCfgData(_cfgFilePath);
            cfgData.StoreCredentials = storeCredentialsFlag;
            SaveCfgData(cfgData);

        }

        public Task<List<int>> GetUserIds()
        {
            throw new NotImplementedException();
        }

        public Task<UserMetaData> SetActiveUser(string login)
        {
            throw new NotImplementedException();
        }


        public Task UpdateUsersList(List<UserMetaData> users)
        {
            throw new NotImplementedException();
        }
    }
}
