namespace Jobzy.Services
{
    using System.Linq;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Profiles.Employers;
    using Jobzy.Web.ViewModels.Profiles.Freelancers;

    public class ProfileManager : IProfileManager
    {
        private readonly IRepository<Employer> employerRepository;
        private readonly IRepository<Freelancer> freelancerRepository;

        public ProfileManager(
            IRepository<Employer> employerRepository,
            IRepository<Freelancer> freelancerRepository)
        {
            this.employerRepository = employerRepository;
            this.freelancerRepository = freelancerRepository;
        }

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
    }
}
