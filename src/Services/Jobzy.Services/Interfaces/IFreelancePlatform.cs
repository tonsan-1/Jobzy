namespace Jobzy.Services.Interfaces
{
    public interface IFreelancePlatform
    {
        IJobManager JobManager { get; }

        IBalanceManager BalanceManager { get; }
    }
}
