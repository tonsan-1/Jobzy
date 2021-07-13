namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatformManager : IFreelancePlatformManager
    {
        public FreelancePlatformManager(
            IJobManager jobManager,
            IBalanceManager balanceManager,
            IOfferManager offerManager)
        {
            this.JobManager = jobManager;
            this.BalanceManager = balanceManager;
            this.OfferManager = offerManager;
        }

        public IJobManager JobManager { get; }

        public IBalanceManager BalanceManager { get; }

        public IOfferManager OfferManager { get; }

    }
}
