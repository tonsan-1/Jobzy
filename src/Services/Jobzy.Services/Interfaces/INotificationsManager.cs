namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface INotificationsManager
    {
        Task CreateAsync(string userId, string icon, string text, string redirectUrl);

        Task<IEnumerable<T>> GetAllUserNotifications<T>(string userId);

        int GetNotificationsCount(string userId);

        Task MarkNotificationAsRead(string notificationId);

        Task MarkAllNotificationsAsRead(string userId);
    }
}
