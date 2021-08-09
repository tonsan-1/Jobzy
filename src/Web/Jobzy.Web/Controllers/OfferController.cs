namespace Jobzy.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Notifications;
    using Jobzy.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class OfferController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public OfferController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Route("/Job/Offers")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult GetJobOffers(string id)
        {
            var offers = this.freelancePlatform.OfferManager.GetJobOffers(id);

            if (offers.Count() == 0)
            {
                return this.View("Error");
            }

            return this.View(offers);
        }

        [HttpPost]
        [Route("/Job/")]
        [Authorize(Roles = "Administrator, Freelancer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOffer(OfferInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("GetJob", "Job", new { id = input.JobId });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var job = await this.freelancePlatform.JobManager.GetJobByIdAsync<JobNotificationViewModel>(input.JobId);

            await this.freelancePlatform.OfferManager.AddAsync(input);
            await this.freelancePlatform.NotificationsManager.CreateAsync(
                job.EmployerId,
                GlobalConstants.OfferIcon,
                $"{user.FirstName} {user.LastName} applied for a job {job.Title}.",
                $"/Job/Offers?id={job.Id}");

            return this.Redirect("/");
        }

        [HttpPost]
        [Route("/Job/Offers")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> AcceptOffer(string offerId, string jobId)
        {
            await this.freelancePlatform.OfferManager.AcceptOffer(offerId);
            await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.InContract, jobId);
            var contractId = await this.freelancePlatform.ContractManager.AddContractAsync(offerId);

            var contract = await this.freelancePlatform
                .ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            await this.freelancePlatform
                .NotificationsManager
                .CreateAsync(
                contract.FreelancerId,
                GlobalConstants.ContractIcon,
                $"{contract.EmployerFirstName} {contract.EmployerLastName} accepted your offer for job {contract.JobTitle} and a contract has been created.",
                $"/Contract?id={contract.Id}");

            return this.Redirect($"/Contract?id={contractId}");
        }
    }
}
