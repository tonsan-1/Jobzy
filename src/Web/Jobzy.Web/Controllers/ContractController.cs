namespace Jobzy.Web.Controllers
{
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
    }
}
