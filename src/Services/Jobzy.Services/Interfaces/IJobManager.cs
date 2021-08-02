namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        SingleJobViewModel GetJobById(string id);

        Task AddAsync(JobInputModel model, string userId);

        Task SetJobStatus(JobStatus status, string jobId);

        IEnumerable<UserJobsListViewModel> GetAllUserJobPosts(string userId);

        IEnumerable<AllJobsListViewModel> GetAllJobPosts();
    }
}
