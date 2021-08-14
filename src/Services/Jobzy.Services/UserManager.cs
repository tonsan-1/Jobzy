namespace Jobzy.Services
{
    using System.Linq;
    using System.Threading.Tasks;

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

        public EmployerViewModel GetEmployer(string userId)
        {
            var employer = this.employerRepository.All()
                .Where(x => x.Id == userId)
                .To<EmployerViewModel>()
                .FirstOrDefault();

            return employer;
        }

        public FreelancerViewModel GetFreelancer(string userId)
        {
            var freelancer = this.freelancerRepository.All()
                .Where(x => x.Id == userId)
                .To<FreelancerViewModel>()
                .FirstOrDefault();

            return freelancer;
        }

        public BaseUserViewModel GetUserSettings(string userId)
        {
            var user = this.baseUserRepository.All()
                .Where(x => x.Id == userId)
                .To<BaseUserViewModel>()
                .FirstOrDefault();

            return user;
        }

        public async Task UpdateUserInfo(UserInfoInputModel input, string userId)
        {
            var user = this.baseUserRepository.All()
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
            var user = await this.baseUserRepository
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
    }
}
