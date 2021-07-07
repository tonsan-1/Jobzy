namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatformManager : IFreelancePlatformManager
    {
        public FreelancePlatformManager(
            IJobManager jobManager,
            IBalanceManager balanceManager)
        {
            this.JobManager = jobManager;
            this.BalanceManager = balanceManager;
        }

        public IJobManager JobManager { get; }

        public IBalanceManager BalanceManager { get; }
    }
}
