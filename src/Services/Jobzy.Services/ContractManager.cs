namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Contracts;

    public class ContractManager : IContractManager
    {
        private readonly IRepository<Offer> offerRepository;
        private readonly IRepository<Contract> contractRepository;

        public ContractManager(
            IRepository<Offer> offerRepository,
            IRepository<Contract> contractRepository)
        {
            this.offerRepository = offerRepository;
            this.contractRepository = contractRepository;
        }

        public async Task<string> AddAsync(string offerId)
        {
            var offer = this.offerRepository.All()
                .FirstOrDefault(x => x.Id == offerId);

            if (offer is null)
            {
                return "Invalid Id";
            }

            var contract = new Contract
            {
                Status = ContractStatus.Ongoing,
                FreelancerId = offer.FreelancerId,
                EmployerId = offer.Job.EmployerId,
                OfferId = offer.Id,
                JobId = offer.Job.Id,
            };

            await this.contractRepository.AddAsync(contract);
            await this.contractRepository.SaveChangesAsync();

            return contract.Id;
        }

        public async Task CompleteContract(string id)
        {
            var contract = this.contractRepository.All()
                .FirstOrDefault(x => x.Id == id);

            contract.Status = ContractStatus.Finished;

            this.contractRepository.Update(contract);
            await this.contractRepository.SaveChangesAsync();
        }

        public async Task CancelContract(string id)
        {
            var contract = this.contractRepository.All()
                .FirstOrDefault(x => x.Id == id);

            contract.Status = ContractStatus.Canceled;

            this.contractRepository.Update(contract);
            await this.contractRepository.SaveChangesAsync();
        }

        public SingleContractViewModel GetContractById(string id)
        {
            var contract = this.contractRepository.All()
                .Where(x => x.Id == id)
                .To<SingleContractViewModel>()
                .FirstOrDefault();

            return contract;
        }

        public IEnumerable<UserContractsListViewModel> GetAllUserContracts(string userId)
        {
            var contracts = this.contractRepository.All()
                .Where(x => x.EmployerId == userId || x.FreelancerId == userId)
                .To<UserContractsListViewModel>()
                .ToList();

            return contracts;
        }
    }
}
