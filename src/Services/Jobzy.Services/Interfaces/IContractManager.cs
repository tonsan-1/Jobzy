namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Contracts;

    public interface IContractManager
    {
        Task<string> AddContractAsync(string offerId);

        SingleContractViewModel GetContractById(string id);

        IEnumerable<UserContractsListViewModel> GetAllUserContracts(string userId);

        Task SetContractStatus(ContractStatus status, string contractId);
    }
}
