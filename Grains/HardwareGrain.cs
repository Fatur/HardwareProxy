using Contracts;
using Orleans;
using System;
using System.Threading.Tasks;

namespace Grains
{
    public class HardwareGrain : Grain,IHardwareGrain
    {
        private ShareStatus mShareStatus;
        private Member mMember;
        private IDisposable mTimer;
        public HardwareGrain()
        {
            mShareStatus = ShareStatus.Ready;
        }

        public Task<Member> GetMember()
        {
            mShareStatus = ShareStatus.Ready;
            return Task.FromResult(mMember);
        }

        public Task<ShareStatus> GetStatus()
        {
            return Task.FromResult(mShareStatus);
        }

        public Task RequestMember(int timeOut)
        {
            if (mShareStatus != ShareStatus.Ready)
                throw new OperationOnInvalidStateException(mShareStatus);
            mShareStatus = ShareStatus.RequestMember;
            mTimer = this.RegisterTimer(_=>BackToReadyState(), this, TimeSpan.FromSeconds(timeOut), TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private Task BackToReadyState()
        {
            this.mShareStatus = ShareStatus.Ready;
            mTimer.Dispose();
            return Task.CompletedTask;
        }

        public Task SendMember(Member member)
        {
            if (mShareStatus != ShareStatus.RequestMember)
                throw new OperationOnInvalidStateException(mShareStatus);
            mMember = member;
            mShareStatus = ShareStatus.MemberReady;
            return Task.CompletedTask;
        }
    }
}
