namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

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

        public async Task<string> CreateAsync(string offerId)
        {
            var offer = await this.offerRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == offerId);

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

        public async Task SetContractStatusAsync(ContractStatus status, string contractId)
        {
            var contract = await this.contractRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == contractId);

            if (status == ContractStatus.Finished)
            {
                contract.CompletedOn = DateTime.UtcNow;
            }

            contract.Status = status;

            this.contractRepository.Update(contract);
            await this.contractRepository.SaveChangesAsync();
        }

        public async Task<T> GetContractByIdAsync<T>(string id)
            => await this.contractRepository.All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllUserContractsAsync<T>(string userId)
            => await this.contractRepository
                .All()
                .Where(x => x.FreelancerId == userId || x.EmployerId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

        public int GetFinishedContractsCount(string userId)
            => this.contractRepository
                .All()
                .Count(
                    x => (x.Status == ContractStatus.Finished && x.FreelancerId == userId) ||
                         (x.Status == ContractStatus.Finished && x.EmployerId == userId));

        public int GetOngoingContractsCount(string userId)
            => this.contractRepository
                .All()
                .Count(
                    x => (x.Status == ContractStatus.Ongoing && x.FreelancerId == userId) ||
                         (x.Status == ContractStatus.Ongoing && x.EmployerId == userId));
    }
}
