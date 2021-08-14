namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        Task<T> GetJobByIdAsync<T>(string id);

        Task AddAsync(JobInputModel model, string userId);

        Task SetJobStatus(JobStatus status, string jobId);

        IEnumerable<UserJobsListViewModel> GetAllUserJobPosts(string userId);

        Task<IEnumerable<T>> GetAllJobPosts<T>(
            string category = null,
            string jobTitle = null,
            Sorting sorting = Sorting.Newest,
            int currentPage = 1);

        int GetPostedJobsCount(string userId);

        int GetAllPostedJobs();
    }
}
