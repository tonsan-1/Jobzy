namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Job;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
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

            await this.freelancePlatform.JobManager.AddAsync(input, currentUser);
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

            var balance = new Balance { User = user };

            await this.freelancePlatform.BalanceManager.AddFundsAsync(balance, money);

            return this.Json("WORKS");
        }
    }
}
