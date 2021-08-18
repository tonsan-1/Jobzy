namespace Jobzy.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Contracts;
    using Jobzy.Web.ViewModels.Notifications;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContractsController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public ContractsController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public async Task<IActionResult> Index(string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            var contract = await this.freelancePlatform.ContractManager.GetContractByIdAsync<SingleContractViewModel>(id);

            if (contract is null ||
                (contract.FreelancerId != userId && contract.EmployerId != userId))
            {
                return this.View("Error");
            }

            return this.View(contract);
        }

        [Authorize(Roles = "Employer, Freelancer")]
        public async Task<IActionResult> MyContracts()
        {
            var userId = this.userManager.GetUserId(this.User);
            var contracts = await this.freelancePlatform.ContractManager
                .GetAllUserContractsAsync<UserContractsListViewModel>(userId);

            this.ViewData["ContractsCount"] = contracts.Count();

            return this.View(contracts);
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public IActionResult CompleteContract(string contractId)
        {
            if (contractId == null)
            {
                return this.View("Error");
            }

            return this.RedirectToAction("Checkout", "Payments", new { id = contractId });
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CancelContract(string contractId)
        {
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<SingleContractViewModel>(contractId);

            await this.freelancePlatform.ContractManager.SetContractStatusAsync(ContractStatus.Canceled, contractId);
            await this.freelancePlatform.JobManager.SetJobStatusAsync(JobStatus.Open, contract.OfferJobId);

            var cancellationNotification = new Notification
            {
                Icon = GlobalConstants.ContractCancelationIcon,
                Text = $"{contract.EmployerFirstName} {contract.EmployerLastName} has cancelled your contract for job {contract.OfferJobTitle}.",
                RedirectAction = "MyContracts",
                RedirectController = "Contracts",
            };

            await this.freelancePlatform.NotificationManager.CreateAsync(cancellationNotification, contract.FreelancerId);

            return this.RedirectToAction("MyContracts", "Contracts");
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> UploadWork([FromForm] IFormFile attachment, string contractId)
        {
            await this.freelancePlatform.FileManager.AddFileToContractAsync(attachment, contractId);
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<SingleContractViewModel>(contractId);

            var notification = new Notification
            {
                Icon = GlobalConstants.ContractAttachmentIcon,
                Text = $"{contract.FreelancerFirstName} {contract.FreelancerLastName} uploaded an attachment to your contract for job {contract.OfferJobTitle}",
                RedirectAction = "Index",
                RedirectController = "Contracts",
                RedirectId = contract.Id,
            };

            await this.freelancePlatform.NotificationManager.CreateAsync(notification, contract.EmployerId);

            return this.RedirectToAction("Index", "Contracts", new { id = contractId });
        }
    }
}
