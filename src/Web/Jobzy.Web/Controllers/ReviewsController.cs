using Microsoft.AspNetCore.Authorization;

namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Reviews;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : Controller
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> LeaveReview(ReviewInputModel input)
        {
            var recipient = await this.userManager.FindByIdAsync(input.RecipientId);
            var sender = await this.userManager.GetUserAsync(this.User);

            if (recipient is null && sender is null)
            {
                return this.View("Error");
            }

            input.SenderId = sender.Id;

            await this.freelancePlatform.ReviewManager.CreateAsync(input);

            if (this.User.IsInRole("Employer"))
            {
                return this.RedirectToAction("Employer", "Users", new { id = recipient.Id });
            }

            return this.RedirectToAction("Freelancer", "Users", new { id = recipient.Id });
        }
    }
}
