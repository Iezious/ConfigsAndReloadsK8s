using System;

namespace ConfigReadApp.AppCode.Logic
{
    public interface IDBConnector
    {
        string GetConnectionString();
        DateTime LastUpdate();
    }
}