namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    public interface IContractManager
    {
        Task<string> AddAsync(string offerId);
    }
}
