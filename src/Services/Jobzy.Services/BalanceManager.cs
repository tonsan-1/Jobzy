namespace Jobzy.Services
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class BalanceManager : IBalanceManager
    {
        private readonly IRepository<Balance> repository;
        private readonly UserManager<ApplicationUser> userManager;

        public BalanceManager(
            IRepository<Balance> repository,
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<bool> AddFundsAsync(Balance balance, decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The amount of replenishment must be greater than 0", nameof(amount));
            }

            if (balance is null)
            {
                throw new ArgumentNullException(nameof(balance));
            }

            balance.Money += amount;
            this.repository.Update(balance);
            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
