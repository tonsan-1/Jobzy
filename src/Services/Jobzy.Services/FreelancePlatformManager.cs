namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatformManager : IFreelancePlatformManager
    {
        public FreelancePlatformManager(
            IJobManager jobManager,
            IBalanceManager balanceManager,
            IProposalManager proposalManager)
        {
            this.JobManager = jobManager;
            this.BalanceManager = balanceManager;
            this.ProposalManager = proposalManager;
        }

        public IJobManager JobManager { get; }

        public IBalanceManager BalanceManager { get; }

        public IProposalManager ProposalManager { get; }

    }
}
