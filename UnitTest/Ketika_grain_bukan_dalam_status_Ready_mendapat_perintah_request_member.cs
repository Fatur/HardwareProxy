using Contracts;
using Orleans.TestingHost;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_bukan_dalam_status_Ready_mendapat_perintah_request_member
    {
        private readonly TestCluster _cluster;
        private IHardwareGrain _proxy;
        public Ketika_grain_bukan_dalam_status_Ready_mendapat_perintah_request_member(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
            _proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(3);
            _proxy.RequestMember(3).Wait();
        }
        [Fact]
        public async Task harus_keluar_error_operation_on_invalid_state()
        {
            var exception = await Assert.ThrowsAsync<OperationOnInvalidStateException>(() => _proxy.RequestMember(3));
            Assert.Equal("Operasi tidak bisa dijalankan pada state RequestMember", exception.Message);
        }
    }
}
