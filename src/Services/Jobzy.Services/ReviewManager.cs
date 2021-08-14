namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Reviews;
    using Microsoft.EntityFrameworkCore;

    public class ReviewManager : IReviewManager
    {
        private readonly IDeletableEntityRepository<Review> repository;

        public ReviewManager(IDeletableEntityRepository<Review> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(ReviewInputModel input)
        {
            var review = new Review
            {
                RecipientId = input.RecipientId,
                SenderId = input.SenderId,
                Rating = input.Rating,
                Text = input.Text,
            };

            await this.repository.AddAsync(review);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllUserReviews<T>(string userId)
        {
            var reviews = await this.repository
                .All()
                .Where(x => x.RecipientId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

            return reviews;
        }

        public int GetReviewsCount(string userId)
        {
            return this.repository.All()
                .Where(x => x.RecipientId == userId)
                .Count();
        }
    }
}
