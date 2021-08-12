namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Profiles;
    using Jobzy.Web.ViewModels.Reviews;
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

        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> Employer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user is null)
            {
                return this.View("Error");
            }

            if (await this.userManager.IsInRoleAsync(user, "Freelancer"))
            {
                return this.RedirectToAction("Freelancer", "Profile", new { id = id });
            }

            var employer = this.freelancePlatform.ProfileManager.GetEmployer(user.Id);

            employer.Reviews = await this.freelancePlatform.ReviewManager.GetAllUserReviews<ReviewsListViewModel>(employer.Id);

            return this.View(employer);
        }

        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> Freelancer(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var user = await this.userManager.FindByIdAsync(id);

            if (user is null)
            {
                return this.View("Error");
            }

            if (await this.userManager.IsInRoleAsync(user, "Employer"))
            {
                return this.RedirectToAction("Employer", "Profile", new { id = id });
            }

            var freelancer = this.freelancePlatform.ProfileManager.GetFreelancer(user.Id);

            freelancer.Reviews = await this.freelancePlatform.ReviewManager.GetAllUserReviews<ReviewsListViewModel>(freelancer.Id);

            return this.View(freelancer);
        }

        [Authorize(Roles = "Freelancer, Employer")]
        public IActionResult Settings()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = this.freelancePlatform.ProfileManager.GetUserSettings(userId);

            var model = new ProfileSettingsViewModel { ProfileViewModel = user };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> UpdateProfileInfo(
            [Bind(Prefix = "ProfileInfoInputModel")] ProfileInfoInputModel input,
            IFormFile profilePicture)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                if (profilePicture is not null && profilePicture.ContentType == "image/jpeg")
                {
                    await this.freelancePlatform.FileManager.UpdateProfilePicture(profilePicture, user.Id);
                }

                var userViewModel = this.freelancePlatform.ProfileManager.GetUserSettings(user.Id);

                return this.View("Settings", new ProfileSettingsViewModel
                {
                    ProfileViewModel = userViewModel,
                });
            }

            if (profilePicture is not null || profilePicture.ContentType == "image/jpeg" || profilePicture.ContentType == "image/png")
            {
                await this.freelancePlatform.FileManager.UpdateProfilePicture(profilePicture, user.Id);
            }

            await this.freelancePlatform.ProfileManager.UpdateUserInfo(input, user.Id);

            if (this.User.IsInRole("Freelancer"))
            {
                return this.RedirectToAction("Freelancer", "Profile", new { id = user.Id });
            }

            return this.RedirectToAction("Employer", "Profile", new { id = user.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer, Employer")]
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

            if (this.User.IsInRole("Freelancer"))
            {
                return this.RedirectToAction("Freelancer", "Profile", new { id = user.Id });
            }

            return this.RedirectToAction("Employer", "Profile", new { id = user.Id });
        }
    }
}
