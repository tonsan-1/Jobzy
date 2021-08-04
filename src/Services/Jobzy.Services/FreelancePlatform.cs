namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatform : IFreelancePlatform
    {
        public FreelancePlatform(
            IJobManager jobManager,
            IOfferManager offerManager,
            IContractManager contractManager,
            IProfileManager profileManager,
            IStripeManager stripeManager,
            ICategoryManager categoryManager,
            IMessageManager messageManager,
            IFileManager fileManager)
        {
            this.JobManager = jobManager;
            this.OfferManager = offerManager;
            this.ContractManager = contractManager;
            this.ProfileManager = profileManager;
            this.StripeManager = stripeManager;
            this.CategoryManager = categoryManager;
            this.MessageManager = messageManager;
            this.FileManager = fileManager;
        }

        public IJobManager JobManager { get; }

        public IOfferManager OfferManager { get; }

        public IContractManager ContractManager { get; }

        public IProfileManager ProfileManager { get; }

        public IStripeManager StripeManager { get; }

        public ICategoryManager CategoryManager { get; }

        public IMessageManager MessageManager { get; }

        public IFileManager FileManager { get; }
    }
}
