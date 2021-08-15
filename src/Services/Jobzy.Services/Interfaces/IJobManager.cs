namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        Task CreateAsync(JobInputModel model, string userId);

        Task SetJobStatusAsync(JobStatus status, string jobId);

        Task<T> GetJobByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllUserJobPostsAsync<T>(string userId);

        Task<IEnumerable<T>> GetAllJobPostsAsync<T>(
            string category = null,
            string jobTitle = null,
            Sorting sorting = Sorting.Newest,
            int currentPage = 1);

        int GetPostedJobsCount(string userId);

        int GetAllPostedJobsCount();
    }
}
