using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Silo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var siloBuilder = new SiloHostBuilder()
            .UseLocalhostClustering()
            .UseDashboard(options => { })
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "ops";
                options.ServiceId = "hardware.proxy";
            })
            .Configure<EndpointOptions>(options =>
                options.AdvertisedIPAddress = IPAddress.Loopback)
            .ConfigureLogging(logging => logging.AddConsole());

            using (var host = siloBuilder.Build())
            {
                await host.StartAsync();

                while (true)
                {
                    Thread.Sleep(10000);
                }
            }
        }
    }
}
