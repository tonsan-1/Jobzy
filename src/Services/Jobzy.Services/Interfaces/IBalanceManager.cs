namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IBalanceManager
    {
        Task<bool> AddFundsAsync(string userId, decimal amount);
    }
}
