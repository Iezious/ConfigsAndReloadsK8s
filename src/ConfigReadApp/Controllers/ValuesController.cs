using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ConfigReadApp.AppCode;
using ConfigReadApp.AppCode.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ConfigReadApp.Controllers
{
    [Route("")]
    public class ValuesController : ControllerBase
    {
        private readonly IOptionsSnapshot<DataConfig> _config;
        private readonly IDBConnector _dbConnector;
        

        public ValuesController(IOptionsSnapshot<DataConfig> config, IOptionsMonitor<DBConfig> dbconfig, IDBConnector dbConnector)
        {
            _config = config;
            _dbConnector = dbConnector;
        }

        [HttpGet("v2")]
        public ActionResult<string> GetV2()
        {
            return _config.Value.V2;
        }

        [HttpGet("v1")]
        public ActionResult<int> GetV1()
        {
            return _config.Value.V1;
        }
        
        [HttpGet("db")]
        public ActionResult<object> GetDb()
        {
            return new
            {
                ConnectionString = _dbConnector.GetConnectionString(),
                LastUpdate = _dbConnector.LastUpdate()
            };
        }
        
        [HttpGet("pid")]
        public ActionResult<string> GetPID()
        {
            return $"{Process.GetCurrentProcess().Id}@{Environment.GetEnvironmentVariable("HOSTNAME")}";
        }

        [HttpGet("healz")]
        public ActionResult<object> GetHealth()
        {
            return new {Ok = 1};
        }
    }
}