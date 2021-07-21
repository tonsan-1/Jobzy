namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OfferController : Controller
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;
        private readonly UserManager<ApplicationUser> userManager;

        public OfferController(
            IFreelancePlatformManager freelancePlatformManager,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
            this.userManager = userManager;
        }

        [Route("/Job/Offers")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult GetJobOffers(string id)
        {
            var offers = this.freelancePlatformManager.OfferManager.GetJobOffers(id);

            return this.View(offers);
        }

        [HttpPost]
        [Route("/Job/")]
        [Authorize(Roles = "Administrator, Freelancer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOffer(string jobId, int fixedPrice, int deliveryDays)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.freelancePlatformManager.OfferManager.AddAsync(jobId, user.Id, fixedPrice, deliveryDays);

            return this.Redirect("/");
        }

        [HttpPost]
        [Route("/Job/Offers")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> AcceptOffer(string offerId, string jobId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var currentUserBalance = this.freelancePlatformManager.BalanceManager.FindById(user.Id);
            var freelancePlatformBalance = await this.freelancePlatformManager.BalanceManager.GetFreelancePlatformBalanceAsync();

            var responseId = await this.freelancePlatformManager.ContractManager.AddAsync(offerId);

            await this.freelancePlatformManager.OfferManager.AcceptOffer(offerId);
            await this.freelancePlatformManager.BalanceManager.TransferMoneyAsync(currentUserBalance, freelancePlatformBalance, offerId);
            await this.freelancePlatformManager.JobManager.SetJobStatus(JobStatus.InContract, jobId);

            return this.Redirect($"/Contract?id={responseId}");
        }
    }
}
