namespace Jobzy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Job;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : BaseController
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IFreelancePlatformManager freelancePlatformManager,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult PostJob() => this.View();

        [HttpPost]
        [Authorize(Roles = "Administrator, Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostJob(JobInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return null;
            }

            if (!(await this.userManager.GetUserAsync(this.User) is Employer currentUser))
            {
                return this.Forbid();
            }

            var freelancePlatformBalance = await this.freelancePlatformManager.BalanceManager.GetFreelancePlatformBalanceAsync();
            var currentUserBalance = this.freelancePlatformManager.BalanceManager.FindById(currentUser.Id);

            try
            {
                await this.freelancePlatformManager.BalanceManager.TransferMoneyAsync(
                    currentUserBalance, freelancePlatformBalance, input.Budget);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error");
            }

            await this.freelancePlatformManager.JobManager.AddAsync(input, currentUser);
            return this.Redirect("/");
        }

        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult AddFunds() => this.View();

        [HttpPost]
        [Authorize(Roles = "Administrator, Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFunds(decimal money)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.freelancePlatformManager.BalanceManager.AddFundsAsync(user.Id, money);

            return this.Json("WORKS");
        }
    }
}
