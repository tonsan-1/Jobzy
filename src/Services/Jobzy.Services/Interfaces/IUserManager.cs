namespace Jobzy.Services.Interfaces
{
    using System.Threading.Tasks;

    using Jobzy.Web.ViewModels.Users;
    using Jobzy.Web.ViewModels.Users.Employers;
    using Jobzy.Web.ViewModels.Users.Freelancers;

    public interface IUserManager
    {
        Task<T> GetUserById<T>(string id);

        EmployerViewModel GetEmployer(string userId);

        FreelancerViewModel GetFreelancer(string userId);

        BaseUserViewModel GetUserSettings(string userId);

        Task UpdateUserProfilePicture(string pictureUrl, string userId);

        Task UpdateUserInfo(UserInfoInputModel input, string userId);

        Task UpdateUserOnlineStatus(string status, string userId);
    }
}
