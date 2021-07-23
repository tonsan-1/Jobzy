namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ProfileController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public ProfileController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Route("/Profile/Employer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public async Task<IActionResult> GetEmployer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (await this.userManager.IsInRoleAsync(user, "Freelancer"))
            {
                return this.Redirect($"/Profile/Freelancer?id={id}");
            }

            var employer = this.freelancePlatform.ProfileManager.GetEmployer(user.Id);

            return this.View(employer);
        }

        [Route("/Profile/Freelancer/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public async Task<IActionResult> GetFreelancer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (await this.userManager.IsInRoleAsync(user, "Employer"))
            {
                return this.Redirect($"/Profile/Employer?id={id}");
            }

            var freelancer = this.freelancePlatform.ProfileManager.GetFreelancer(user.Id);

            return this.View(freelancer);
        }
    }
}
