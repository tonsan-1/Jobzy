namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;

    public class NotificationsManager : INotificationsManager
    {
        private readonly IDeletableEntityRepository<Notification> repository;

        public NotificationsManager(IDeletableEntityRepository<Notification> repository)
        {
            this.repository = repository;
        }

        public Task CreateAsync(string userId, string text, string redirectUrl)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllUserNotifications<T>(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task MarkAllNotificationsAsRead(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task MarkNotificationAsRead(string notificationId)
        {
            throw new System.NotImplementedException();
        }

        public int GetNotificationsCount(string userId)
        {
            throw new System.NotImplementedException();
        }
    }
}
