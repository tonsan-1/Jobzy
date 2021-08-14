namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Jobzy.Data.Common.Repositories;
    using global::Jobzy.Services.Interfaces;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Jobs;
    using Microsoft.EntityFrameworkCore;

    public class JobManager : IJobManager
    {
        public const int JobsPerPage = 8;
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

        public async Task<IEnumerable<T>> GetAllJobPosts<T>(
            string category = null,
            string jobTitle = null,
            Sorting sorting = Sorting.Newest,
            int currentPage = 1)
        {

            var jobsQuery = this.repository
                .All()
                .Where(x => x.Status == JobStatus.Open)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                jobsQuery = jobsQuery.Where(x => x.Category.Name.ToLower() == category.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(jobTitle))
            {
                jobsQuery = jobsQuery.Where(x => x.Title.ToLower().Contains(jobTitle.ToLower()));
            }

            jobsQuery = sorting switch
            {
                Sorting.Oldest => jobsQuery.OrderBy(x => x.CreatedOn),
                Sorting.Random => jobsQuery.OrderBy(x => Guid.NewGuid()),
                Sorting.Newest or _ => jobsQuery.OrderByDescending(x => x.CreatedOn),
            };

            var jobs = await jobsQuery
                .Skip((currentPage - 1) * JobsPerPage)
                .Take(JobsPerPage)
                .To<T>()
                .ToListAsync();

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

        public async Task SetJobStatus(JobStatus status, string jobId)
        {
            var job = this.repository.All().FirstOrDefault(x => x.Id == jobId);

            job.Status = status;

            this.repository.Update(job);
            await this.repository.SaveChangesAsync();
        }

        public async Task<T> GetJobByIdAsync<T>(string id)
           => await this.repository.All()
               .Where(x => x.Id == id)
               .To<T>()
               .FirstOrDefaultAsync();

        public int GetPostedJobsCount(string userId)
            => this.repository.All()
                .Where(x => x.EmployerId == userId)
                .Count();

        public int GetAllPostedJobs()
            => this.repository
                .All()
                .Count();
    }
}
