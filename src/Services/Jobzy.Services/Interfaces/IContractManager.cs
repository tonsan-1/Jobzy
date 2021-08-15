namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;

    public interface IContractManager
    {
        Task<string> CreateAsync(string offerId);

        Task SetContractStatusAsync(ContractStatus status, string contractId);

        Task<T> GetContractByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllUserContractsAsync<T>(string userId);

        int GetFinishedContractsCount(string userId);

        int GetOngoingContractsCount(string userId);
    }
}
