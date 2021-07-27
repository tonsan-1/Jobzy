namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatform : IFreelancePlatform
    {
        public FreelancePlatform(
            IJobManager jobManager,
            IBalanceManager balanceManager,
            IOfferManager offerManager,
            IContractManager contractManager,
            IProfileManager profileManager,
            IStripeManager stripeManager)
        {
            this.JobManager = jobManager;
            this.BalanceManager = balanceManager;
            this.OfferManager = offerManager;
            this.ContractManager = contractManager;
            this.ProfileManager = profileManager;
            this.StripeManager = stripeManager;
        }

        public IJobManager JobManager { get; }

        public IBalanceManager BalanceManager { get; }

        public IOfferManager OfferManager { get; }

        public IContractManager ContractManager { get; }

        public IProfileManager ProfileManager { get; }

        public IStripeManager StripeManager { get; }
    }
}
