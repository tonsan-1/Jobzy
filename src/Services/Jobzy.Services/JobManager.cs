namespace Jobzy.Services
{
    using System.Threading.Tasks;

    using global::Jobzy.Data.Common.Repositories;
    using global::Jobzy.Services.Interfaces;
    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Job;
    using Microsoft.AspNetCore.Identity;

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
    }
}
