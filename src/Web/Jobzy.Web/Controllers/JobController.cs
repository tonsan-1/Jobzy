namespace Jobzy.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Jobs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class JobController : Controller
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;
        private readonly UserManager<ApplicationUser> userManager;

        public JobController(
            IFreelancePlatformManager freelancePlatformManager,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
            this.userManager = userManager;
        }

        [Route("/Job/All")]
        [Authorize(Roles = "Administrator, Freelancer")]
        public IActionResult GetAllJobs()
        {
            var jobs = this.freelancePlatformManager.JobManager.GetAllJobPosts();

            return this.View(jobs);
        }

        [Route("/Job/")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetJob(string id)
        {
            var job = this.freelancePlatformManager.JobManager.GetJobById(id);

            return this.View(job);
        }

        [Route("/Job/MyJobs")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> GetMyJobs()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var jobs = this.freelancePlatformManager.JobManager.GetAllUserJobPosts(user.Id);

            return this.View(jobs);
        }

        [Route("/Job/Add")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult AddJob() => this.View();

        [HttpPost]
        [Route("/Job/Add")]
        [Authorize(Roles = "Administrator, Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddJob(JobInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return null;
            }

            if (!(await this.userManager.GetUserAsync(this.User) is Employer currentUser))
            {
                return this.Forbid();
            }

            await this.freelancePlatformManager.JobManager.AddAsync(input, currentUser);
            return this.Redirect("/");
        }
    }
}
