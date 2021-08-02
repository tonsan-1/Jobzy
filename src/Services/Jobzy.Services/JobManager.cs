namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Jobzy.Data.Common.Repositories;
    using global::Jobzy.Services.Interfaces;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Jobs;

    public class JobManager : IJobManager
    {
        private readonly IRepository<Job> repository;

        public JobManager(IRepository<Job> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(JobInputModel model, string userId)
        {
            var job = new Job
            {
                Status = JobStatus.Open,
                EmployerId = userId,
                Title = model.Title,
                CategoryId = model.CategoryId,
                Budget = model.Budget,
                Description = model.Description,
            };

            await this.repository.AddAsync(job);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<AllJobsListViewModel> GetAllJobPosts()
        {
            var jobs = this.repository.All()
                .Where(x => !x.IsDeleted && x.Status == JobStatus.Open)
                .To<AllJobsListViewModel>()
                .ToList();

            return jobs;
        }

        public IEnumerable<UserJobsListViewModel> GetAllUserJobPosts(string userId)
        {
            var jobs = this.repository.All()
                .Where(x => x.Employer.Id == userId)
                 .To<UserJobsListViewModel>()
                .ToList();

            return jobs;
        }

        public SingleJobViewModel GetJobById(string id)
        {
            var job = this.repository.All()
                .Where(x => x.Id == id)
                .To<SingleJobViewModel>()
                .FirstOrDefault();

            return job;
        }

        public async Task SetJobStatus(JobStatus status, string jobId)
        {
            var job = this.repository.All().FirstOrDefault(x => x.Id == jobId);

            job.Status = status;

            this.repository.Update(job);
            await this.repository.SaveChangesAsync();
        }
    }
}
