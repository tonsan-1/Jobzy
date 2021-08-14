namespace Jobzy.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Users;
    using Jobzy.Web.ViewModels.Users.Employers;
    using Jobzy.Web.ViewModels.Users.Freelancers;
    using Microsoft.EntityFrameworkCore;

    public class UserManager : IUserManager
    {
        private const int FreelancersPerPage = 8;
        private readonly IRepository<Employer> employerRepository;
        private readonly IRepository<Freelancer> freelancerRepository;
        private readonly IRepository<ApplicationUser> baseUserRepository;

        public UserManager(
            IRepository<Employer> employerRepository,
            IRepository<Freelancer> freelancerRepository,
            IRepository<ApplicationUser> baseUserRepository)
        {
            this.employerRepository = employerRepository;
            this.freelancerRepository = freelancerRepository;
            this.baseUserRepository = baseUserRepository;
        }

        public async Task<T> GetUserById<T>(string id)
            => await this.baseUserRepository
                .All()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<T>> GetAllFreelancers<T>(
            int rating = 0,
            string name = null,
            Sorting sorting = Sorting.Newest,
            int currentPage = 1)
        {
            var freelancersQuery =
                this.freelancerRepository
                .All()
                .AsQueryable();

            if (rating > 0 && rating <= 5)
            {
                freelancersQuery =
                    freelancersQuery
                    .Where(x => Math.Round(x.ReceivedReviews.Average(x => x.Rating)) >= rating);
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                freelancersQuery =
                    freelancersQuery
                    .Where(x =>
                    x.FirstName.ToLower().Contains(name.ToLower()) ||
                    x.LastName.ToLower().Contains(name.ToLower()));
            }

            freelancersQuery = sorting switch
            {
                Sorting.Oldest => freelancersQuery.OrderBy(x => x.CreatedOn),
                Sorting.Random => freelancersQuery.OrderBy(x => Guid.NewGuid()),
                Sorting.Newest or _ => freelancersQuery.OrderByDescending(x => x.CreatedOn),
            };

            var freelancers =
                await freelancersQuery
                .Skip((currentPage - 1) * FreelancersPerPage)
                .Take(FreelancersPerPage)
                .To<T>()
                .ToListAsync();

            return freelancers;
        }

        public EmployerViewModel GetEmployer(string userId)
        {
            var employer =
                this.employerRepository
                .All()
                .Where(x => x.Id == userId)
                .To<EmployerViewModel>()
                .FirstOrDefault();

            return employer;
        }

        public FreelancerViewModel GetFreelancer(string userId)
        {
            var freelancer =
                this.freelancerRepository
                .All()
                .Where(x => x.Id == userId)
                .To<FreelancerViewModel>()
                .FirstOrDefault();

            return freelancer;
        }

        public BaseUserViewModel GetUserSettings(string userId)
        {
            var user =
                this.baseUserRepository
                .All()
                .Where(x => x.Id == userId)
                .To<BaseUserViewModel>()
                .FirstOrDefault();

            return user;
        }

        public async Task UpdateUserInfo(UserInfoInputModel input, string userId)
        {
            var user =
                this.baseUserRepository
                .All()
                .FirstOrDefault(x => x.Id == userId);

            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Location = input.Location;
            user.Description = input.Description;
            user.TagName = input.TagName;

            this.baseUserRepository.Update(user);
            await this.baseUserRepository.SaveChangesAsync();
        }

        public Task UpdateUserProfilePicture(string pictureUrl, string userId)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateUserOnlineStatus(string status, string userId)
        {
            var user =
                await this.baseUserRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (status == "online")
            {
                user.IsOnline = true;
            }
            else if (status == "offline")
            {
                user.IsOnline = false;
            }

            this.baseUserRepository.Update(user);
            await this.baseUserRepository.SaveChangesAsync();
        }

        public int GetAllFreelancersCount()
            => this.freelancerRepository
                .All()
                .Count();
    }
}
