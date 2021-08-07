namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Offers;

    public interface IOfferManager
    {
        Task AddAsync(OfferInputModel input);

        Task AcceptOffer(string offerId);

        IEnumerable<JobOfferViewModel> GetJobOffers(string jobId);

        int GetSentOffersCount(string userId);
    }
}
