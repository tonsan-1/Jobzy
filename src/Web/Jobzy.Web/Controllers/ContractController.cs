namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ContractController : Controller
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;

        public ContractController(IFreelancePlatformManager freelancePlatformManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
        }

        [Route("/Contract/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult Contract(string id)
        {
            var contract = this.freelancePlatformManager.ContractManager.GetContractById(id);

            return this.View(contract);
        }

        [HttpPost]
        [Route("/Contract/")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> CompleteOrCancelContract(string action, string contractId)
        {
            if (action == "complete")
            {
                await this.freelancePlatformManager.ContractManager.CompleteContract(contractId);
            }
            else
            {
                await this.freelancePlatformManager.ContractManager.CancelContract(contractId);
            }

            return this.Redirect("/");
        }
    }
}
