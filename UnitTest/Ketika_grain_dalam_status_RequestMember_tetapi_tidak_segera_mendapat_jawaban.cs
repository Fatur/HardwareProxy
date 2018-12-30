using Contracts;
using Orleans.TestingHost;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    [Collection(ClusterCollection.Name)]
    public class Ketika_grain_dalam_status_RequestMember_tetapi_tidak_segera_mendapat_jawaban
    {
        private readonly TestCluster _cluster;
        private IHardwareGrain _proxy;
        public Ketika_grain_dalam_status_RequestMember_tetapi_tidak_segera_mendapat_jawaban(ClusterFixture fixture)
        {
            _cluster = fixture.Cluster;
            _proxy = _cluster.GrainFactory.GetGrain<IHardwareGrain>(4);
            _proxy.RequestMember(3).Wait();

            
        }
        [Fact]
        public async Task Setelah_time_out_harus_kembali_ke_status_ready()
        {
            var status = await _proxy.GetStatus();
            Assert.Equal(ShareStatus.RequestMember, status);
            Thread.Sleep(5000);
            var statusAfter5Sec = await _proxy.GetStatus();
            Assert.Equal(ShareStatus.Ready, statusAfter5Sec);
        }
        
    }
}
