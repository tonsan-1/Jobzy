namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Contracts;

    public interface IContractManager
    {
        Task<string> AddAsync(string offerId);

        ContractViewModel GetContractById(string id);
    }
}
