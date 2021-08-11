namespace Jobzy.Web.Controllers
{
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
            var contracts = this.freelancePlatform.ContractManager.GetAllUserContracts<UserContractsListViewModel>(user.Id);

            return this.View(contracts);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> ContractActions(string action, string contractId, string jobId)
        { // use input model insted of strings and separate the logic in two separate methods
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            if (action == "cancel")
            {
                await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Canceled, contractId);
                await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.Open, jobId);
                await this.freelancePlatform.NotificationsManager.CreateAsync(
                contract.FreelancerId,
                GlobalConstants.ContractCancelationIcon,
                $"{contract.EmployerFirstName} {contract.EmployerLastName} canceled your contract for job {contract.JobTitle}",
                $"/Contracts/Index/{contract.Id}");

                return this.RedirectToAction("All", "Contracts");
            }

            await this.freelancePlatform.NotificationsManager.CreateAsync(
                contract.FreelancerId,
                GlobalConstants.ContractCompletetionIcon,
                $"{contract.EmployerFirstName} {contract.EmployerLastName} completed your contract for job {contract.JobTitle}",
                $"/Contracts/Index/{contract.Id}");

            return this.RedirectToAction("Checkout", "Payments", new { id = contractId });
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer")]
        public async Task<IActionResult> UploadWork([FromForm]IFormFile attachment, string contractId)
        {
            // some validations of the file size and type

            await this.freelancePlatform.FileManager.AddAttachmentToContract(attachment, contractId);
            var contract = await this.freelancePlatform.ContractManager
                .GetContractByIdAsync<ContractNotificationViewModel>(contractId);

            await this.freelancePlatform.NotificationsManager.CreateAsync(
                contract.EmployerId,
                GlobalConstants.ContractAttachmentIcon,
                $"{contract.FreelancerFirstName} {contract.FreelancerLastName} uploaded work to your contract for {contract.JobTitle}",
                $"/Contracts/Index/{contract.Id}");

            return this.RedirectToAction("Index", "Contracts", new { id = contractId });
        }
    }
}
