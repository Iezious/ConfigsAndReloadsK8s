using System;
using Microsoft.Extensions.Options;

namespace ConfigReadApp.AppCode.Logic
{
    public class DBConnector : IDBConnector
    {
        private DBConfig _currentConfig;
        private DateTime _lastRead;

        public DBConnector(IOptionsMonitor<DBConfig> configMonitor)
        {
            _currentConfig = configMonitor.CurrentValue;
            _lastRead = DateTime.Now;

            configMonitor.OnChange((newConfig) =>
            {
                _currentConfig = newConfig; 
                _lastRead = DateTime.Now;
            });
        }

        public string GetConnectionString()
        {
            return _currentConfig.ConnectionString;
        }

        public DateTime LastUpdate()
        {
            return _lastRead;
        }
    }
}