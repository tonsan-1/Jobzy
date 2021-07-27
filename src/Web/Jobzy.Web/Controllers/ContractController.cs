namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
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
            var contract = this.freelancePlatform.ContractManager.GetContractById(id);

            return this.View(contract);
        }

        [HttpPost]
        [Route("/Contract/")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> ContractActions(
            string action, string contractId, string jobId)
        {
            if (action == "cancel")
            {
                await this.freelancePlatform.ContractManager.SetContractStatus(ContractStatus.Canceled, contractId);
                await this.freelancePlatform.JobManager.SetJobStatus(JobStatus.Open, jobId);

                return this.RedirectToAction("GetMyContracts", "ContractController");
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
    }
}
