namespace Jobzy.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : BaseController
    {
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult PostJob()
        {
            return this.View();
        }
    }
}
