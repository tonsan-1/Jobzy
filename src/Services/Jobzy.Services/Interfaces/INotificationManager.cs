namespace Jobzy.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;

    public interface INotificationManager
    {
        Task CreateAsync(Notification notification, string userId);

        Task MarkNotificationAsReadAsync(string notificationId);

        Task MarkAllNotificationsAsReadAsync(string userId);

        Task<IEnumerable<T>> GetAllUserNotificationsAsync<T>(string userId);

        int GetNotificationsCount(string userId);
    }
}
