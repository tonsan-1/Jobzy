namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Employer, Freelancer")]
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

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (this.User.IsInRole("Employer"))
            {
                var employer = new EmployerDashboardViewModel
                {
                    Name = user.FirstName,
                    FinishedContractsCount = this.freelancePlatform.ContractManager.GetFinishedContractsCount(user.Id),
                    OngoingContractsCount = this.freelancePlatform.ContractManager.GetOngoingContractsCount(user.Id),
                    JobPostedCount = this.freelancePlatform.JobManager.GetPostedJobsCount(user.Id),
                    ReviewsCount = this.freelancePlatform.ReviewManager.GetReviewsCount(user.Id),
                };

                return this.View("Employer", employer);
            }
            else if (this.User.IsInRole("Freelancer"))
            {
                var freelancer = new FreelancerDashboardViewModel
                {
                    Name = user.FirstName,
                    TotalMoneyEarned = this.freelancePlatform.StripeManager.GetFreelancerBalanceAmount(user.Id),
                    OngoingContractsCount = this.freelancePlatform.ContractManager.GetOngoingContractsCount(user.Id),
                    FinishedContractsCount = this.freelancePlatform.ContractManager.GetFinishedContractsCount(user.Id),
                    ReviewsCount = this.freelancePlatform.ReviewManager.GetReviewsCount(user.Id),
                    SentOffersCount = this.freelancePlatform.OfferManager.GetSentOffersCount(user.Id),
                };

                return this.View("Freelancer", freelancer);
            }

            return this.View("Error");
        }
    }
}
