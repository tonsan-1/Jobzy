namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Job;

    public interface IJobManager
    {
        Task AddAsync(JobInputModel model, Employer employer);

        IEnumerable<PostedJobsListViewModel> GetAllUserJobPosts(string userId);
    }
}
