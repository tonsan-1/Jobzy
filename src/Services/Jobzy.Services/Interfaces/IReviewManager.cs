namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Reviews;

    public interface IReviewManager
    {
        Task<IEnumerable<T>> GetAllUserReviews<T>(string userId);

        Task CreateAsync(ReviewInputModel input);

        int GetReviewsCount(string userId);
    }
}
