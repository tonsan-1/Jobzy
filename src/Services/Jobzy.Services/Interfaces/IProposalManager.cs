namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Jobs;

    public interface IProposalManager
    {
        Task AddAsync(string jobId, string userId);

        IEnumerable<JobProposalsViewModel> GetJobProposals(string jobId);
    }
}
