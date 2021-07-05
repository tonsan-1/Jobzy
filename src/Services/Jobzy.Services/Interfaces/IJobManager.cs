namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Web.ViewModels.Job;

    public interface IJobManager
    {
        public Task AddAsync(JobInputModel model, Employer employer);
    }
}
