namespace Jobzy.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;

    public interface IBalanceManager
    {
        Task<bool> AddFundsAsync(string userId, decimal amount);

        Task<Balance> GetFreelancePlatformBalanceAsync();

        Balance FindById(string userId);

        Task<bool> TransferMoneyAsync(Balance senderBalance, Balance receipientBalance, decimal amount);
    }
}
