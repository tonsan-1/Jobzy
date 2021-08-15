namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Reviews;
    using Jobzy.Web.ViewModels.Users;
    using Jobzy.Web.ViewModels.Users.Employers;
    using Jobzy.Web.ViewModels.Users.Freelancers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(
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
                return this.RedirectToAction("Freelancer", "Users", new { id = id });
            }

            var employer = await this.freelancePlatform.UserManager
                .GetEmployerByIdAsync<EmployerViewModel>(user.Id);

            employer.Reviews = await this.freelancePlatform.ReviewManager
                .GetAllUserReviewsAsync<ReviewsListViewModel>(employer.Id);

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
                return this.RedirectToAction("Employer", "Users", new { id = id });
            }

            var freelancer = await this.freelancePlatform.UserManager
                .GetFreelancerByIdAsync<FreelancerViewModel>(user.Id);

            freelancer.Reviews = await this.freelancePlatform.ReviewManager
                .GetAllUserReviewsAsync<ReviewsListViewModel>(freelancer.Id);

            return this.View(freelancer);
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> AllFreelancers([FromQuery] AllFreelancersQueryModel query)
        {
            query.Freelancers = await this.freelancePlatform.UserManager
                .GetAllFreelancersAsync<FreelancerViewModel>(
                    query.Rating,
                    query.Name,
                    query.Sorting,
                    query.CurrentPage);

            foreach (var freelancer in query.Freelancers)
            {
                freelancer.Reviews = await this.freelancePlatform.ReviewManager
                        .GetAllUserReviewsAsync<ReviewsListViewModel>(freelancer.Id);
            }

            return this.View(query);
        }

        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> Settings()
        {
            var userId = this.userManager.GetUserId(this.User);

            var user = await this.freelancePlatform.UserManager
                .GetUserByIdAsync<BaseUserViewModel>(userId);

            var model = new UserSettingsViewModel { UserViewModel = user };

            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> UpdateProfileInfo(
            [Bind(Prefix = "UserInfoInputModel")] UserInfoInputModel input,
            IFormFile profilePicture)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                if (profilePicture is not null && profilePicture.ContentType == "image/jpeg")
                {
                    var pictureUrl = await this.freelancePlatform.FileManager
                        .UploadAttachmentAsync(profilePicture);
                    await this.freelancePlatform.UserManager
                        .UpdateUserProfilePictureAsync(pictureUrl, user.Id);
                }

                var userViewModel = await this.freelancePlatform.UserManager
                    .GetUserByIdAsync<BaseUserViewModel>(user.Id);

                return this.View("Settings", new UserSettingsViewModel
                {
                    UserViewModel = userViewModel,
                });
            }

            if (profilePicture is not null)
            {
                if (profilePicture.ContentType == "image/jpeg" || profilePicture.ContentType == "image/png")
                {
                    var pictureUrl = await this.freelancePlatform.FileManager
                        .UploadAttachmentAsync(profilePicture);
                    await this.freelancePlatform.UserManager
                        .UpdateUserProfilePictureAsync(pictureUrl, user.Id);
                }
            }

            await this.freelancePlatform.UserManager.UpdateUserInfoAsync(input, user.Id);

            if (this.User.IsInRole("Freelancer"))
            {
                return this.RedirectToAction("Freelancer", "Users", new { id = user.Id });
            }

            return this.RedirectToAction("Employer", "Users", new { id = user.Id });
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> ChangePassword(
            [Bind(Prefix = "UserPasswordInputModel")] UserPasswordInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var userViewModel = await this.freelancePlatform.UserManager
                .GetUserByIdAsync<BaseUserViewModel>(user.Id);

            if (!this.ModelState.IsValid)
            {
                return this.View("Settings", new UserSettingsViewModel
                {
                    UserViewModel = userViewModel,
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

                return this.View("Settings", new UserSettingsViewModel
                {
                    UserViewModel = userViewModel,
                });
            }

            if (this.User.IsInRole("Freelancer"))
            {
                return this.RedirectToAction("Freelancer", "Users", new { id = user.Id });
            }

            return this.RedirectToAction("Employer", "Users", new { id = user.Id });
        }
    }
}
