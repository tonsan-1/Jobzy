namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Common;
    using Jobzy.Web.ViewModels.Users;

    public interface IUserManager
    {
        Task UpdateUserProfilePictureAsync(string pictureUrl, string userId);

        Task UpdateUserInfoAsync(UserInfoInputModel input, string userId);

        Task UpdateUserOnlineStatusAsync(string status, string userId);

        Task<T> GetUserByIdAsync<T>(string id);

        Task<IEnumerable<T>> GetAllFreelancersAsync<T>(
            int rating = 0,
            string name = null,
            Sorting sorting = Sorting.Newest,
            int currentPage = 1);

        Task<T> GetEmployerByIdAsync<T>(string userId);

        Task<T> GetFreelancerByIdAsync<T>(string userId);

        int GetAllFreelancersCount();
    }
}
