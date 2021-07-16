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
        private readonly IRepository<Balance> balanceRepository;
        private readonly IRepository<Offer> offerRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public BalanceManager(
            IRepository<Balance> balanceRepository,
            IRepository<Offer> offerRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.balanceRepository = balanceRepository;
            this.offerRepository = offerRepository;
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
            this.balanceRepository.Update(balance);
            await this.balanceRepository.SaveChangesAsync();

            return true;
        }

        public Balance FindById(string userId)
            => this.balanceRepository.All().FirstOrDefault(x => x.UserId == userId);

        public async Task<Balance> GetFreelancePlatformBalanceAsync()
        {
            var admin = await this.userManager.FindByEmailAsync("admin@admin.com");

            freelancePlatformBalance = this.FindById(admin.Id);

            return freelancePlatformBalance;
        }

        public async Task<bool> TransferMoneyAsync(Balance senderBalance, Balance recipientBalance, string offerId)
        {
            var offer = this.offerRepository.All()
                .FirstOrDefault(x => x.Id == offerId);

            senderBalance.Money -= offer.FixedPrice;
            recipientBalance.Money += offer.FixedPrice;

            this.balanceRepository.Update(senderBalance);
            this.balanceRepository.Update(recipientBalance);

            await this.balanceRepository.SaveChangesAsync();

            return true;
        }
    }
}
