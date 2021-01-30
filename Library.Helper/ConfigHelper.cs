using Newtonsoft.Json;
using System.Collections.Generic;

namespace Library.Helper
{
    public class ConfigHelper : Interface.IConfigHelper
    {
        private Dictionary<ConfigSystemType, string> dictionary = new Dictionary<ConfigSystemType, string>();
        public ConfigHelper()
        {
            dictionary = GetConfigurationFromFile(EnvironmentType.Dev);
        }
        public string GetConfiguration(ConfigSystemType systemType)
        {
            return dictionary[systemType];
        }

        public T GetConfiguration<T>(ConfigSystemType systemType)
        {
            var configString = GetConfiguration(systemType);
            T result = JsonConvert.DeserializeObject<T>(configString);
            return result;
        }

        private Dictionary<ConfigSystemType, string> GetConfigurationFromFile(EnvironmentType dev)
        {
            var fileName = "Library.Helpers.Environment.cf";

            if (!System.IO.File.Exists(fileName))
            {
                //eklenecek
            }

            #region Dosyaya yazılacak
            var mydictionary = new Dictionary<ConfigSystemType, string>();
            mydictionary.Add(ConfigSystemType.RabbitMq, @"
                    {
                          'Host': 'localhost',
                          'Port': 5672,
                          'User': 'admin',
                          'PassWord': 'admin'
                    }");
            mydictionary.Add(ConfigSystemType.MsSqlDb, @"
                {
                      'User': 'sa',
                      'Password': 'secretP4ssword',
                      'Hostname': 'localhost',
                      'Port': '14334',
                      'Database': 'MicroServicesSampleDb'
                }");
            mydictionary.Add(ConfigSystemType.MongoDb, @"
                {
                      'HostUrl': 'mongodb://localhost:27017',
                      'Database': 'MicroServicesSampleDb'
                }");
          
            //mydictionary.Add(ConfigSystemType.AuthService, @"
            //    {
            //          'Url': 'http://localhost:51228/',
            //          'GetAuthKeyMethodPath':'api/Transform/GetLedger'
            //    }");
          
            #endregion
            return mydictionary;
        }

    }

    public enum ConfigSystemType
    {
        None = 0,
        RabbitMq = 1,
        MongoDb = 2,
        MsSqlDb = 3,
        AuthService = 4,
        FileService = 5,
        MailService = 6,
    }

    public enum EnvironmentType
    {
        None = 0,
        Dev = 1,
        Test = 2,
        Production = 3//live
    }
}
