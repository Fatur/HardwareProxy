using Contracts;
using Orleans.TestingHost;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_dalam_status_RequestMember_mendapat_kiriman_data_member
    {
        private readonly TestCluster _cluster;
        private IHardwareGrain _proxy;
        public Ketika_grain_dalam_status_RequestMember_mendapat_kiriman_data_member(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
            _proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(4);
            _proxy.RequestMember().Wait();

            _proxy.SendMember(new Member { MemberNo = "7898", Name = "Mabuk RR" }).Wait();
        }
        [Fact]
        public async Task Status_grain_harus_member_ready()
        {
            var status = await _proxy.GetStatus();
            Assert.Equal(ShareStatus.MemberReady, status);
            var member = await _proxy.GetMember();
            Assert.Equal("7898", member.MemberNo);
            Assert.Equal("Mabuk RR", member.Name);
        }
        
    }
}
