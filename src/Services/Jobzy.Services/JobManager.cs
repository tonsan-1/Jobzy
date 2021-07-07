namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Jobzy.Data.Common.Repositories;
    using global::Jobzy.Services.Interfaces;
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

        public async Task AddAsync(JobInputModel model, Employer employer)
        {
            var job = new Job
            {
                Employer = employer,
                Title = model.Title,
                JobType = model.JobType,
                JobCategory = model.JobCategory,
                Budget = model.Budget,
                Description = model.Description,
            };

            await this.repository.AddAsync(job);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<PostedJobsListViewModel> GetAllUserJobPosts(string userId)
        {
            var jobs = this.repository.All()
                .Where(x => x.Employer.Id == userId)
                .To<PostedJobsListViewModel>()
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
    }
}
