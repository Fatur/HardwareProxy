using Contracts;
using Orleans.TestingHost;
using System;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_pertama_kali_dibuat
    {
        private readonly TestCluster _cluster;
        public Ketika_grain_pertama_kali_dibuat(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }
        [Fact]
        public void Status_grain_harus_ready()
        {
            var proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(0);
            var status = proxy.GetStatus().Result;
            Assert.Equal(ShareStatus.Ready, status);
        }
    }
}
