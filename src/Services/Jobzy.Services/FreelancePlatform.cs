namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatform : IFreelancePlatform
    {
        public FreelancePlatform(
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
