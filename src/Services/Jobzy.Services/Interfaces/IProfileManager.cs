namespace Jobzy.Services.Interfaces
{
    using Jobzy.Web.ViewModels.Profiles.Employers;
    using Jobzy.Web.ViewModels.Profiles.Freelancers;

    public interface IProfileManager
    {
        EmployerViewModel GetEmployer(string userId);

        FreelancerViewModel GetFreelancer(string userId);
    }
}
