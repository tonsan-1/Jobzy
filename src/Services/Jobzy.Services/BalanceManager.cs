namespace Jobzy.Services
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;

    public class BalanceManager : IBalanceManager
    {
        private static Balance freelancePlatformBalance;
        private readonly IRepository<Balance> repository;
        private readonly UserManager<ApplicationUser> userManager;

        public BalanceManager(
            IRepository<Balance> repository,
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<bool> AddFundsAsync(string userId, decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The amount of replenishment must be greater than 0", nameof(amount));
            }

            var balance = this.FindById(userId);

            if (balance is null)
            {
                throw new ArgumentNullException(nameof(balance));
            }

            balance.Money += amount;
            this.repository.Update(balance);
            await this.repository.SaveChangesAsync();

            return true;
        }

        public Balance FindById(string userId)
            => this.repository.All().FirstOrDefault(x => x.UserId == userId);

        public async Task<Balance> GetFreelancePlatformBalanceAsync()
        {
            var admin = await this.userManager.FindByEmailAsync("admin@admin.com");

            freelancePlatformBalance = this.FindById(admin.Id);

            return freelancePlatformBalance;
        }

        public async Task<bool> TransferMoneyAsync(Balance senderBalance, Balance recipientBalance, decimal amount)
        {
            if (senderBalance is null)
            {
                throw new ArgumentNullException(nameof(senderBalance));
            }

            if (recipientBalance is null)
            {
                throw new ArgumentNullException(nameof(recipientBalance));
            }

            if (amount <= 0)
            {
                throw new ArgumentException("The amount must be greater than 0", nameof(amount));
            }

            if (senderBalance.Money < amount)
            {
                throw new ArgumentException($"The user {senderBalance.User?.Name ?? "freelancing platform"} hasn't got enough money for this operation");
            }

            senderBalance.Money -= amount;
            recipientBalance.Money += amount;

            this.repository.Update(senderBalance);
            this.repository.Update(recipientBalance);

            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
