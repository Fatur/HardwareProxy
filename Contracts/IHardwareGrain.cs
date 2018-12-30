using Orleans;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHardwareGrain : IGrainWithIntegerKey
    {
        Task<ShareStatus> GetStatus();
        Task RequestMember();
        Task SendMember(Member member);
        Task<Member> GetMember();
    }
}
