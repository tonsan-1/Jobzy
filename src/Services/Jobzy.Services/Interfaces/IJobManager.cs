namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        Task AddAsync(JobInputModel model, Employer employer);

        IEnumerable<UserJobsListViewModel> GetAllUserJobPosts(string userId);

        IEnumerable<AllJobsListViewModel> GetAllJobPosts();

        SingleJobViewModel GetJobById(string id);
    }
}
