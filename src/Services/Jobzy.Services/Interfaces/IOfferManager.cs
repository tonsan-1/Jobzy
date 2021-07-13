namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Jobs;

    public interface IOfferManager
    {
        Task AddAsync(string jobId, string userId, decimal fixedPrice, int deliveryDays);

        IEnumerable<JobOfferViewModel> GetJobOffers(string jobId);
    }
}
