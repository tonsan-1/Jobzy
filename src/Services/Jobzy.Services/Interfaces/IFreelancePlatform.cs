namespace Jobzy.Services.Interfaces
{
    public interface IFreelancePlatform
    {
        IJobManager JobManager { get; }

        IBalanceManager BalanceManager { get; }

        IOfferManager OfferManager { get; }

        IContractManager ContractManager { get; }
    }
}
