namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ContractController : Controller
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;
        private readonly UserManager<ApplicationUser> userManager;

        public ContractController(
            IFreelancePlatformManager freelancePlatformManager,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
            this.userManager = userManager;
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
        public async Task<IActionResult> CompleteOrCancelContract(
            string action, string contractId, string offerId, string freelancerId, string jobId)
        {
            var user =
                    await this.userManager.GetUserAsync(this.User);
            var freelancePlatformBudget =
                await this.freelancePlatformManager.BalanceManager.GetFreelancePlatformBalanceAsync();
            var currentUserBudget =
                this.freelancePlatformManager.BalanceManager.FindById(user.Id);
            var freelancerBudget =
                this.freelancePlatformManager.BalanceManager.FindById(freelancerId);

            if (action == "complete")
            {
                await this.freelancePlatformManager.BalanceManager.TransferMoneyAsync(freelancePlatformBudget, freelancerBudget, offerId);
                await this.freelancePlatformManager.ContractManager.CompleteContract(contractId);
                await this.freelancePlatformManager.JobManager.SetJobToClosed(jobId);
            }
            else
            {
                await this.freelancePlatformManager.BalanceManager.TransferMoneyAsync(freelancePlatformBudget, currentUserBudget, offerId);
                await this.freelancePlatformManager.ContractManager.CancelContract(contractId);
                await this.freelancePlatformManager.JobManager.SetJobToOpen(jobId);
            }

            return this.Redirect("/");
        }
    }
}
