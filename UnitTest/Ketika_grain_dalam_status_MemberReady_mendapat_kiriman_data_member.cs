using Contracts;
using Orleans.TestingHost;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_dalam_status_MemberReady_mendapat_kiriman_data_member
    {
        private readonly TestCluster _cluster;
        private IHardwareGrain _proxy;
        public Ketika_grain_dalam_status_MemberReady_mendapat_kiriman_data_member(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
            _proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(2);
            _proxy.RequestMember().Wait();

            _proxy.SendMember(new Member { MemberNo = "7898", Name = "Mabuk RR" }).Wait();
        }
        [Fact]
        public async Task harus_mengeluarkan_error_OperationOnInvalidState()
        {
            var exception= await Assert.ThrowsAsync<OperationOnInvalidStateException>(()=>_proxy.SendMember(new Member { MemberNo = "7898", Name = "Mabuk RR" }));
            Assert.Equal("Operasi tidak bisa dijalankan pada state MemberReady", exception.Message);
        }
        
    }
}
