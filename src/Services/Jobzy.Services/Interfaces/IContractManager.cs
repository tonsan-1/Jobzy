namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Contracts;

    public interface IContractManager
    {
        Task<string> AddAsync(string offerId);

        SingleContractViewModel GetContractById(string id);

        IEnumerable<UserContractsListViewModel> GetAllUserContracts(string userId);

        Task CompleteContract(string id);

        Task CancelContract(string id);
    }
}
