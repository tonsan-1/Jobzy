namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Jobs;

    public class ProposalManager : IProposalManager
    {
        private readonly IRepository<Proposal> repository;

        public ProposalManager( IRepository<Proposal> repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(string jobId, string userId, decimal fixedPrice, int deliveryDays)
        {
            var proposal = new Proposal
            {
                JobId = jobId,
                FreelancerId = userId,
                FixedPrice = fixedPrice,
                DeliveryDays = deliveryDays,
            };

            await this.repository.AddAsync(proposal);
            await this.repository.SaveChangesAsync();
        }

        public IEnumerable<JobProposalsViewModel> GetJobProposals(string jobId)
        {
            var proposals = this.repository.All()
                .Where(x => x.JobId == jobId)
                .To<JobProposalsViewModel>()
                .ToList();

            return proposals;
        }
    }
}
