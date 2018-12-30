using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest
{
    public class ClusterFixture : IDisposable
    {
        public TestCluster Cluster { get; private set; }
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder(2);
            builder.Options.ServiceId = Guid.NewGuid().ToString();
            builder.ConfigureHostConfiguration(TestDefaultConfiguration.ConfigureHostConfiguration);
            this.Cluster = builder.Build();
            this.Cluster.Deploy();
        }
        public void Dispose()
        {
            this.Cluster.StopAllSilos();
        }
    }
}
