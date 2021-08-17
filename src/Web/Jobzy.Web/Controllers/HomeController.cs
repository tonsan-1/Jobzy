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

        public HomeController(IFreelancePlatform freelancePlatform)
        {
            this.freelancePlatform = freelancePlatform;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel
            {
                JobsCount = this.freelancePlatform.JobManager.GetAllPostedJobsCount(),
                FreelancersCount = this.freelancePlatform.UserManager.GetAllFreelancersCount(),
                OffersCount = this.freelancePlatform.OfferManager.GetAllOffersCount(),
            };

            if (this.User.Identity.IsAuthenticated == false)
            {
                return this.View(model);
            }

            if (this.User.IsInRole("Administrator"))
            {
                return this.RedirectToAction("Index", "Dashboard", new { area = "Administration" });
            }

            if (this.User.IsInRole("Employer"))
            {
                model.Freelancers = await this.freelancePlatform.UserManager
                        .GetAllFreelancersAsync<FreelancerViewModel>(rating: 4, sorting: Sorting.Random);

                foreach (var freelancer in model.Freelancers)
                {
                    freelancer.Reviews = await this.freelancePlatform.ReviewManager
                            .GetAllUserReviewsAsync<ReviewsListViewModel>(freelancer.Id);
                }

                if (model.Freelancers.Any())
                {
                    model.Freelancers = model.Freelancers.Take(6).ToList();
                }
            }

            if (this.User.IsInRole("Freelancer"))
            {
                model.Jobs = await this.freelancePlatform.JobManager
                    .GetAllJobPostsAsync<AllJobsListViewModel>(sorting: Sorting.Random);

                model.Jobs = model.Jobs.Take(6).ToList();
            }

            return this.View(model);
        }

        public IActionResult Privacy() => this.View();
    }
}
