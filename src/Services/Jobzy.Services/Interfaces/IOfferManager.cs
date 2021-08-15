namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Offers;

    public interface IOfferManager
    {
        Task CreateAsync(OfferInputModel input);

        Task AcceptOfferAsync(string offerId);

        Task DeleteOfferAsync(string offerId);

        Task<T> GetJobOfferByIdAsync<T>(string offerId);

        Task<IEnumerable<T>> GetJobOffersAsync<T>(string jobId);

        Task<IEnumerable<T>> GetUserJobOffersAsync<T>(string userId);

        int GetActiveOffersCount(string userId);

        int GetSentOffersCount(string userId);

        int GetAllOffersCount();
    }
}
