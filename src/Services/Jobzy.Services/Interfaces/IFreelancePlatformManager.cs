namespace Jobzy.Services.Interfaces
{
    public interface IFreelancePlatformManager
    {
        IJobManager JobManager { get; }

        IBalanceManager BalanceManager { get; }

        IOfferManager OfferManager { get; }
    }
}
