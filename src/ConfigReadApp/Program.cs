using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Winton.Extensions.Configuration.Consul;

namespace ConfigReadApp
{
    public class Program
    {
        private static CancellationTokenSource _stopper;
        
        public static void Main(string[] args)
        {
            _stopper = new CancellationTokenSource();
            CreateWebHostBuilder(args).Build().Run();
            _stopper.Cancel();
                
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("/var/config/config.dc.json", true, true);
                    cb.AddJsonFile("config.dc.json", true, true);
                    cb.AddJsonFile("/var/config/config.app.json", true, true);
                    cb.AddJsonFile("config.app.json", true, true);
                    cb.AddConsul("K8SConfigs/config.dc", _stopper.Token, source =>
                    {
                        source.Optional = true;
                        source.ReloadOnChange = true;
                        source.ConsulConfigurationOptions =
                            cco => { cco.Address = new Uri(Environment.GetEnvironmentVariable("CONSUL_URL") ?? "http://localhost:8500"); };
                    });
                    cb.AddConsul("K8SConfigs/config.app", _stopper.Token, source =>
                    {
                        source.Optional = true;
                        source.ReloadOnChange = true;
                        source.ConsulConfigurationOptions =
                            cco => { cco.Address = new Uri(Environment.GetEnvironmentVariable("CONSUL_URL") ?? "http://localhost:8500"); };
                    });
                })
                .UseStartup<Startup>();
    }
}