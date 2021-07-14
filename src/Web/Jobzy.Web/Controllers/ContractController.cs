namespace Jobzy.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ContractController : Controller
    {
        [Route("/Contract/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult Contract(string id)
        {
            return this.View();
        }
    }
}
