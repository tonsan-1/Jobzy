namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContractController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public ContractController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Route("/Contract/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetContract(string id)
        {
            // validate if current user is in the contract

            var contract = this.freelancePlatform.ContractManager.GetContractById(id);

            if (contract is null)
            {
                return this.View("Error");
            }

            return this.View(contract);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> ContractActions(string action, string contractId, string jobId)
        { // use input model insted of strings
            if (action == "cancel")
            {
                await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Canceled, contractId);
                await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.Open, jobId);

                // TODO:notify both parties that the contract is canceled
                return this.RedirectToAction("GetMyContracts", "Contract");
            }

            return this.Redirect($"/Checkout?id={contractId}");
        }

        [Route("/Contract/MyContracts")]
        [Authorize(Roles = "Administrator, Employer, Freelancer")]
        public async Task<IActionResult> GetMyContracts()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var contracts = this.freelancePlatform.ContractManager.GetAllUserContracts(user.Id);

            return this.View(contracts);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Freelancer")]
        public async Task<IActionResult> UploadFile([FromForm]IFormFile file)
        {
            // some validations of the file size and type

            var uploadedFileUrl = await this.freelancePlatform.FileManager.UploadFile(file);

            return this.Redirect("/");
        }
    }
}
