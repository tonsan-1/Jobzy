namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;

    public interface INotificationsManager
    {
        Task CreateAsync(Notification notification, string userId);

        Task<IEnumerable<T>> GetAllUserNotifications<T>(string userId);

        int GetNotificationsCount(string userId);

        Task MarkNotificationAsRead(string notificationId);

        Task MarkAllNotificationsAsRead(string userId);
    }
}
