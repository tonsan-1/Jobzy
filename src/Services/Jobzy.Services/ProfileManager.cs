namespace Jobzy.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Profiles;
    using Jobzy.Web.ViewModels.Profiles.Employers;
    using Jobzy.Web.ViewModels.Profiles.Freelancers;
    using Microsoft.EntityFrameworkCore;

    public class ProfileManager : IProfileManager
    {
        private readonly IRepository<Employer> employerRepository;
        private readonly IRepository<Freelancer> freelancerRepository;
        private readonly IRepository<ApplicationUser> baseUserRepository;

        public ProfileManager(
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

        public BaseProfileViewModel GetUserSettings(string userId)
        {
            var user = this.baseUserRepository.All()
                .Where(x => x.Id == userId)
                .To<BaseProfileViewModel>()
                .FirstOrDefault();

            return user;
        }

        public async Task UpdateUserInfo(ProfileInfoInputModel input, string userId)
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
    }
}
