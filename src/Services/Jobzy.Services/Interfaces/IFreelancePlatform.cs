namespace Jobzy.Services.Interfaces
{
    public interface IFreelancePlatform
    {
        IJobManager JobManager { get; }

        ICategoryManager CategoryManager { get; }

        IOfferManager OfferManager { get; }

        IContractManager ContractManager { get; }

        IUserManager UserManager { get; }

        IStripeManager StripeManager { get; }

        IMessageManager MessageManager { get; }

        IFileManager FileManager { get; }

        IReviewManager ReviewManager { get; }

        INotificationsManager NotificationsManager { get; }
    }
}
