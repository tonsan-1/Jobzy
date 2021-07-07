namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Jobs;

    public interface IJobManager
    {
        Task AddAsync(JobInputModel model, Employer employer);

        IEnumerable<PostedJobsListViewModel> GetAllUserJobPosts(string userId);

        SingleJobViewModel GetJobById(string id);
    }
}
