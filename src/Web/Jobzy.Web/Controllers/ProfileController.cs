namespace Jobzy.Web.Controllers
{
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;

        //add rolemanager to check user id before giving it to the manager

        public ProfileController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        [Route("/Profile/Employer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetEmployer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var employer = this.freelancePlatform.ProfileManager.GetEmployer(id);

            return this.View(employer);
        }

        [Route("/Profile/Freelancer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetFreelancer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            return this.View();
        }
    }
}
