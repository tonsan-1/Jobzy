namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class JobsController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> Index(string id)
        {
            var job = await this.freelancePlatform.JobManager
                .GetJobByIdAsync<SingleJobViewModel>(id);

            if (job is null)
            {
                return this.View("Error");
            }

            return this.View(job);
        }

        [Authorize(Roles = "Freelancer, Employer")]
        public async Task<IActionResult> All([FromQuery] AllJobsQueryModel query)
        {
            var jobs = await this.freelancePlatform.JobManager
                .GetAllJobPosts<AllJobsListViewModel>(
                query.Category,
                query.JobTitle,
                query.Sorting,
                query.CurrentPage);

            var categories = await this.freelancePlatform.CategoryManager
                .GetAllJobCategories<CategoriesListViewModel>();

            query.Jobs = jobs;
            query.Categories = categories;

            return this.View(query);
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> MyJobs()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var jobs = this.freelancePlatform.JobManager.GetAllUserJobPosts(user.Id);

            return this.View(jobs);
        }

        [Authorize(Roles = "Employer")]
        public async Task<IActionResult> AddJob()
        {
            var jobCategories = await this.freelancePlatform.CategoryManager
                .GetAllJobCategories<CategoriesListViewModel>();

            return this.View(new JobInputModel { Categories = jobCategories });
        }

        [HttpPost]
        [Authorize(Roles = "Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJob(JobInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var jobCategories = await this.freelancePlatform.CategoryManager
                    .GetAllJobCategories<CategoriesListViewModel>();

                return this.View(new JobInputModel { Categories = jobCategories });
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.freelancePlatform.JobManager.AddAsync(input, user.Id);
            return this.RedirectToAction("MyJobs", "Jobs");
        }
    }
}
