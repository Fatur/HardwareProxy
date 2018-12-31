using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orleans;

namespace ProxyApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private IClusterClient mOrleansClient;
        public HardwareController(IClusterClient orleansClient)
        {
            mOrleansClient = orleansClient;
        }
        [HttpPut]
        public async Task RequestMember(int hardwareId)
        {
            var proxy = this.mOrleansClient.GetGrain<IHardwareGrain>(hardwareId);
            await proxy.RequestMember(30);
        }
        [HttpPut]
        public async Task SendMember(int hardwareId,string memberNo, string memberName)
        {
            var proxy = this.mOrleansClient.GetGrain<IHardwareGrain>(hardwareId);
            await proxy.SendMember(new Member { MemberNo = memberNo, Name = memberName });
        }
        [HttpGet]
        public async Task<Member> GetMember(int hardwareId)
        {
            var proxy = this.mOrleansClient.GetGrain<IHardwareGrain>(hardwareId);
            return await proxy.GetMember();
        }
        [HttpGet]
        public async Task<ShareStatus> GetStatus(int hardwareId)
        {
            var proxy = this.mOrleansClient.GetGrain<IHardwareGrain>(hardwareId);
            return await proxy.GetStatus();
        }
    }
}