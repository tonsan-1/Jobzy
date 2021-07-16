namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        SingleJobViewModel GetJobById(string id);

        Task AddAsync(JobInputModel model, Employer employer);

        Task SetJobToClosed(string jobId);

        Task SetJobToOpen(string jobId);

        Task SetContractIdToJob(string jobId, string contractId);

        IEnumerable<UserJobsListViewModel> GetAllUserJobPosts(string userId);

        IEnumerable<AllJobsListViewModel> GetAllJobPosts();
    }
}
