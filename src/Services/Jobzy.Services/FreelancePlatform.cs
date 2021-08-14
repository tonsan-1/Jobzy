namespace Jobzy.Services
{
    using Jobzy.Services.Interfaces;

    public class FreelancePlatform : IFreelancePlatform
    {
        public FreelancePlatform(
            IJobManager jobManager,
            IOfferManager offerManager,
            IContractManager contractManager,
            IUserManager userManager,
            IStripeManager stripeManager,
            ICategoryManager categoryManager,
            IMessageManager messageManager,
            IFileManager fileManager,
            IReviewManager reviewManager,
            INotificationsManager notificationsManager)
        {
            this.JobManager = jobManager;
            this.OfferManager = offerManager;
            this.ContractManager = contractManager;
            this.UserManager = userManager;
            this.StripeManager = stripeManager;
            this.CategoryManager = categoryManager;
            this.MessageManager = messageManager;
            this.FileManager = fileManager;
            this.ReviewManager = reviewManager;
            this.NotificationsManager = notificationsManager;
        }

        public IJobManager JobManager { get; }

        public IOfferManager OfferManager { get; }

        public IContractManager ContractManager { get; }

        public IUserManager UserManager { get; }

        public IStripeManager StripeManager { get; }

        public ICategoryManager CategoryManager { get; }

        public IMessageManager MessageManager { get; }

        public IFileManager FileManager { get; }

        public IReviewManager ReviewManager { get; }

        public INotificationsManager NotificationsManager { get; }
    }
}
