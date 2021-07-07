namespace Jobzy.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;

    public class BalanceManager : IBalanceManager
    {
        private readonly IRepository<Balance> repository;

        public BalanceManager(IRepository<Balance> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> AddFundsAsync(string userId, decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The amount of replenishment must be greater than 0", nameof(amount));
            }

            var balance = this.repository.All()
                .FirstOrDefault(x => x.UserId == userId);

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
