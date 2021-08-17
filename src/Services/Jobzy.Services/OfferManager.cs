namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Offers;
    using Microsoft.EntityFrameworkCore;

    public class OfferManager : IOfferManager
    {
        private readonly IDeletableEntityRepository<Offer> repository;

        public OfferManager(IDeletableEntityRepository<Offer> repository)
        {
            this.repository = repository;
        }

        public async Task AcceptOfferAsync(string offerId)
        {
            var offer = this.repository.All()
                .FirstOrDefault(x => x.Id == offerId);

            offer.AcceptedDate = DateTime.UtcNow;
            offer.IsAccepted = true;

            this.repository.Update(offer);
            await this.repository.SaveChangesAsync();
        }

        public async Task CreateAsync(OfferInputModel input)
        {
            var offer = new Offer
            {
                JobId = input.JobId,
                FreelancerId = input.UserId,
                FixedPrice = input.FixedPrice,
                DeliveryDays = input.DeliveryDays,
            };

            await this.repository.AddAsync(offer);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteOfferAsync(string offerId)
        {
            var offer = await this.repository
                .All()
                .FirstOrDefaultAsync(x => x.Id == offerId);

            this.repository.Delete(offer);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetJobOffersAsync<T>(string jobId)
            => await this.repository.All()
                .Where(x => x.JobId == jobId && !x.IsAccepted)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> GetUserJobOffersAsync<T>(string userId)
            => await this.repository
                .All()
                .Where(x => x.FreelancerId == userId && !x.IsAccepted)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<T> GetJobOfferByIdAsync<T>(string offerId)
            => await this.repository
                .All()
                .Where(x => x.Id == offerId)
                .To<T>()
                .FirstOrDefaultAsync();

        public int GetActiveOffersCount(string userId)
            => this.repository
                .All()
                .Count(x => x.FreelancerId == userId && !x.IsAccepted);

        public int GetSentOffersCount(string userId)
            => this.repository
                .All()
                .Count(x => x.FreelancerId == userId);

        public int GetAllOffersCount()
            => this.repository
                .All()
                .Count();
    }
}
