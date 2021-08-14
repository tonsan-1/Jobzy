namespace Jobzy.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Common.Repositories;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Services.Mapping;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class NotificationManager : INotificationManager
    {
        private readonly IDeletableEntityRepository<Notification> repository;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationManager(
            IDeletableEntityRepository<Notification> repository,
            UserManager<ApplicationUser> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task CreateAsync(Notification notification, string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            notification.Users.Add(user);

            await this.repository.AddAsync(notification);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllUserNotifications<T>(string userId)
        {
            var notificatons = await this.repository
                .All()
                .Where(x => x.Users.Any(x => x.Id == userId) && !x.IsRead)
                .Include(x => x.Users)
                .OrderByDescending(x => x.CreatedOn)
                .To<T>()
                .ToListAsync();

            return notificatons;
        }

        public async Task MarkAllNotificationsAsRead(string userId)
        {
            var notifications = await this.repository
                .All()
                .Where(x => x.Users.Any(x => x.Id == userId))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;

                this.repository.Update(notification);
                await this.repository.SaveChangesAsync();
            }
        }

        public async Task MarkNotificationAsRead(string notificationId)
        {
            var notification = await this.repository
                .All()
                .FirstOrDefaultAsync(x => x.Id == notificationId);

            notification.IsRead = true;

            this.repository.Update(notification);
            await this.repository.SaveChangesAsync();
        }

        public int GetNotificationsCount(string userId)
        {
            return this.repository
                .All()
                .Where(x => x.Users.Any(x => x.Id == userId) && !x.IsRead)
                .Count();
        }
    }
}
