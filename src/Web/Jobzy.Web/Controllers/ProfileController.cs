namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Profiles;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
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

        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult Settings()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = this.freelancePlatform.ProfileManager.GetUserSettings(userId);

            var model = new ProfileSettingsViewModel { ProfileViewModel = user };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public async Task<IActionResult> UpdateProfileInfo(
            [Bind(Prefix = "ProfileInfoInputModel")]ProfileInfoInputModel input,
            IFormFile profilePicture)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                var userViewModel = this.freelancePlatform.ProfileManager.GetUserSettings(user.Id);

                return this.View("Settings", new ProfileSettingsViewModel
                {
                    ProfileViewModel = userViewModel,
                });
            }

            if (profilePicture is not null && profilePicture.ContentType == "image/jpeg")
            {
                await this.freelancePlatform.FileManager.UpdateProfilePicture(profilePicture, user.Id);
            }

            await this.freelancePlatform.ProfileManager.UpdateUserInfo(input, user.Id);

            return this.Redirect("/");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public async Task<IActionResult> ChangePassword(
            [Bind(Prefix = "ProfilePasswordInputModel")] ProfilePasswordInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userViewModel = this.freelancePlatform.ProfileManager.GetUserSettings(user.Id);

            if (!this.ModelState.IsValid)
            {
                return this.View("Settings", new ProfileSettingsViewModel
                {
                    ProfileViewModel = userViewModel,
                });
            }

            var result = await this.userManager.ChangePasswordAsync(
                user, input.CurrentPassword, input.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    this.ModelState.AddModelError("input.CurrentPassword", error.Description);
                }

                return this.View("Settings", new ProfileSettingsViewModel
                {
                    ProfileViewModel = userViewModel,
                });
            }

            return this.Redirect("/");
        }
    }
}
