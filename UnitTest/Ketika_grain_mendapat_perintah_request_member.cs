using Contracts;
using Orleans.TestingHost;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_mendapat_perintah_request_member
    {
        private readonly TestCluster _cluster;
        public Ketika_grain_mendapat_perintah_request_member(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
        }
        [Fact]
        public async Task Status_grain_harus_request_member()
        {
            var proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(1);
            await proxy.RequestMember(3);
            var status = proxy.GetStatus().Result;
            Assert.Equal(ShareStatus.RequestMember, status);
        }
    }
}
