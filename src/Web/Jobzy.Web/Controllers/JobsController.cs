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

    public class JobsController : Controller
    {
        private readonly IFreelancePlatformManager freelancePlatformManager;
        private readonly UserManager<ApplicationUser> userManager;

        public JobsController(
            IFreelancePlatformManager freelancePlatformManager,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatformManager = freelancePlatformManager;
            this.userManager = userManager;
        }

        [Route("/Jobs/{id}")]
        [Authorize(Roles = "Administrator, Freelancer, Employer")]
        public IActionResult GetJob(string id)
        {
            var job = this.freelancePlatformManager.JobManager.GetJobById(id);

            return this.View(job);
        }

        [HttpPost]
        [Route("/Jobs/{id}")]
        [Authorize(Roles = "Administrator, Freelancer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendProposal(string jobId, int fixedPrice, int deliveryDays)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.freelancePlatformManager.ProposalManager.AddAsync(jobId, user.Id, fixedPrice, deliveryDays);

            return this.Redirect("/");
        }

        [Route("/Jobs/All")]
        [Authorize(Roles = "Administrator, Freelancer")]
        public IActionResult All()
        {
            var jobs = this.freelancePlatformManager.JobManager.GetAllJobPosts();

            return this.View(jobs);
        }

        [Route("/Dashboard/Jobs/MyJobs")]
        [Authorize(Roles = "Administrator, Employer")]
        public async Task<IActionResult> MyJobs()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var jobs = this.freelancePlatformManager.JobManager.GetAllUserJobPosts(user.Id);

            return this.View(jobs);
        }

        [Route("/Dashboard/Jobs/ManageCandidates/{id}")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult ManageCandidates(string id)
        {
            var proposals = this.freelancePlatformManager.ProposalManager.GetJobProposals(id);

            return this.View(proposals);
        }

        [HttpPost]
        [Route("/Dashboard/Jobs/ManageCandidates/{id}")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult ApproveCandidate(string jobId, string freelancerId)
        {

            return this.Json("Works");
        }

        [Route("/Jobs/TestContract")]
        public IActionResult TestContract() => this.View();

        [Route("/Dashboard/Jobs/Add")]
        [Authorize(Roles = "Administrator, Employer")]
        public IActionResult Add() => this.View();

        [HttpPost]
        [Route("/Dashboard/Jobs/Add")]
        [Authorize(Roles = "Administrator, Employer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(JobInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return null;
            }

            if (!(await this.userManager.GetUserAsync(this.User) is Employer currentUser))
            {
                return this.Forbid();
            }

            var freelancePlatformBalance = await this.freelancePlatformManager.BalanceManager.GetFreelancePlatformBalanceAsync();
            var currentUserBalance = this.freelancePlatformManager.BalanceManager.FindById(currentUser.Id);

            try
            {
                await this.freelancePlatformManager.BalanceManager.TransferMoneyAsync(
                    currentUserBalance, freelancePlatformBalance, input.Budget);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error");
            }

            await this.freelancePlatformManager.JobManager.AddAsync(input, currentUser);
            return this.Redirect("/");
        }
    }
}
