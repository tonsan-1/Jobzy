namespace Jobzy.Services
{
    using System.Linq;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;

    public class ReviewManager : IReviewManager
    {
        private readonly IRepository<Review> repository;

        public ReviewManager(IRepository<Review> repository)
        {
            this.repository = repository;
        }

        public int GetReviewsCount(string userId)
        {
            return this.repository.All()
                .Where(x => x.UserId == userId)
                .Count();
        }
    }
}
