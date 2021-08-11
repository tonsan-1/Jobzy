namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Offers;

    public interface IOfferManager
    {
        Task AddAsync(OfferInputModel input);

        Task AcceptOffer(string offerId);

        Task DeleteOffer(string offerId);

        IEnumerable<JobOfferViewModel> GetJobOffers(string jobId);

        Task<IEnumerable<T>> GetUserJobOffers<T>(string userId);

        int GetSentOffersCount(string userId);
    }
}
