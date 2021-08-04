namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Profiles;
    using Jobzy.Web.ViewModels.Profiles.Employers;
    using Jobzy.Web.ViewModels.Profiles.Freelancers;

    public interface IProfileManager
    {
        EmployerViewModel GetEmployer(string userId);

        FreelancerViewModel GetFreelancer(string userId);

        BaseProfileViewModel GetUserSettings(string userId);

        Task UpdateUserProfilePicture(string pictureUrl, string userId);

        Task UpdateUserInfo(ProfileInfoInputModel input, string userId);
    }
}
