namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Contracts;

    public interface IContractManager
    {
        Task<string> AddContractAsync(string offerId);

        Task<T> GetContractByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllUserContracts<T>(string userId);

        Task SetContractStatus(ContractStatus status, string contractId);

        int GetFinishedContractsCount(string userId);

        int GetOngoingContractsCount(string userId);
    }
}
