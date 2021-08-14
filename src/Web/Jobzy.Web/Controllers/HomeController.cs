namespace Jobzy.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Home;
    using Jobzy.Web.ViewModels.Jobs;
    using Jobzy.Web.ViewModels.Reviews;
    using Jobzy.Web.ViewModels.Users.Freelancers;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var model = new HomeViewModel
            {
                JobsCount = this.freelancePlatform.JobManager.GetAllPostedJobs(),
                FreelancersCount = this.freelancePlatform.UserManager.GetAllFreelancersCount(),
                OffersCount = this.freelancePlatform.OfferManager.GetAllOffersCount(),
            };

            if (user is null)
            {
                return this.View(model);
            }

            if (this.User.IsInRole("Employer"))
            {
                model.Freelancers =
                    await this.freelancePlatform
                        .UserManager
                        .GetHighestRatedFreelancers<FreelancerViewModel>();

                foreach (var freelancer in model.Freelancers)
                {
                    freelancer.Reviews =
                        await this.freelancePlatform
                            .ReviewManager
                            .GetAllUserReviews<ReviewsListViewModel>(freelancer.Id);
                }
            }

            if (this.User.IsInRole("Freelancer"))
            {
                model.Jobs =
                    await this.freelancePlatform.JobManager.GetAllJobPosts<AllJobsListViewModel>(
                        sorting: JobSorting.Random);

                model.Jobs = model.Jobs.Take(6);
            }

            return this.View(model);
        }
    }
}
