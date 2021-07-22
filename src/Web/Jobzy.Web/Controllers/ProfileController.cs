namespace Jobzy.Web.Controllers
{
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        public ProfileController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [Route("/Profile/Employer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetEmployer(string id)
        {
            return this.View();
        }

        [Route("/Profile/Employer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetFreelancer(string id)
        {
            return this.View();
        }
    }
}
