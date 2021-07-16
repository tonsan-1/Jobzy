namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Jobs;

    public interface IOfferManager
    {
        Task AddAsync(string jobId, string userId, decimal fixedPrice, int deliveryDays);

        Task AcceptOffer(string offerId);

        IEnumerable<JobOfferViewModel> GetJobOffers(string jobId);
    }
}
