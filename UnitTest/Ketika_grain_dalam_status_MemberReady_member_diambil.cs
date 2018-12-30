using Contracts;
using Orleans.TestingHost;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_dalam_status_MemberReady_member_diambil
    {
        private readonly TestCluster _cluster;
        private IHardwareGrain _proxy;
        public Ketika_grain_dalam_status_MemberReady_member_diambil(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
            _proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(5);
            _proxy.RequestMember().Wait();

            _proxy.SendMember(new Member { MemberNo = "7898", Name = "Mabuk RR" }).Wait();
        }
        [Fact]
        public async Task state_harus_berubah_menjadi_ready()
        {
            var member=await _proxy.GetMember();
            var status = await _proxy.GetStatus();
            Assert.Equal(ShareStatus.Ready, status);
        }
        
    }
}
