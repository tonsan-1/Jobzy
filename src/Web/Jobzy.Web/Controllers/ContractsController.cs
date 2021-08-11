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
            // validate if current user is in the contract

            var contract = await this.freelancePlatform.ContractManager.GetContractByIdAsync<SingleContractViewModel>(id);

            if (contract is null)
            {
                return this.View("Error");
            }

            return this.View(contract);
        }

        [Authorize(Roles = "Employer, Freelancer")]
        public async Task<IActionResult> MyContracts()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var contracts = await this.freelancePlatform.ContractManager.GetAllUserContracts<UserContractsListViewModel>(user.Id);

            this.ViewData["ContractsCount"] = contracts.Count();

            return this.View(contracts);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Employer")]
        [ValidateAntiForgeryToken]
        public IActionResult ContractActions(string action, string contractId)
        { // use input model insted of strings and separate the logic in two separate methods
            if (action == "cancel")
            {
                return this.RedirectToAction("CancelContract", "Contracts");
            }

            return this.RedirectToAction("Checkout", "Payments", new { id = contractId });
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompleteContract([FromBody] string contractId)
        {
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Finished, contractId);

            var completionNotification = new Notification
            {
                Icon = GlobalConstants.ContractCompletetionIcon,
                Text = $"{contract.EmployerFirstName} {contract.EmployerLastName} completed your contract for job {contract.JobTitle}",
                RedirectAction = "MyContracts",
                RedirectController = "Contracts",
            };

            await this.freelancePlatform.NotificationsManager.CreateAsync(completionNotification, contract.FreelancerId);

            return this.RedirectToAction("MyContracts", "Contracts");
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> CancelContract([FromBody] string contractId)
        {
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Canceled, contractId);
            await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.Open, contract.JobId);

            var cancellationNotification = new Notification
            {
                Icon = GlobalConstants.ContractCancelationIcon,
                Text = $"{contract.EmployerFirstName} {contract.EmployerLastName} has cancelled your contract for job {contract.JobTitle}.",
                RedirectAction = "MyContracts",
                RedirectController = "Contracts",
            };

            await this.freelancePlatform.NotificationsManager.CreateAsync(cancellationNotification, contract.FreelancerId);

            return this.RedirectToAction("MyContracts", "Contracts");
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWork([FromForm] IFormFile attachment, string contractId)
        {
            // some validations of the file size and type

            await this.freelancePlatform.FileManager.AddAttachmentToContract(attachment, contractId);
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            var notification = new Notification
            {
                Icon = GlobalConstants.ContractAttachmentIcon,
                Text = $"{contract.FreelancerFirstName} {contract.FreelancerLastName} uploaded an attachment to your contract for job {contract.JobTitle}",
                RedirectAction = "Index",
                RedirectController = "Contracts",
                RedirectId = contract.Id,
            };

            await this.freelancePlatform.NotificationsManager.CreateAsync(notification, contract.EmployerId);

            return this.RedirectToAction("Index", "Contracts", new { id = contractId });
        }
    }
}
