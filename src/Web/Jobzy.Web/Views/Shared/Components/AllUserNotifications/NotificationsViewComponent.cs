namespace Jobzy.Web.Views.Shared.Components.AllUserNotifications
{
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Notifications;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "AllUserNotifications")]
    public class NotificationsViewComponent : ViewComponent
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationsViewComponent(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = this.userManager.GetUserId(this.UserClaimsPrincipal);

            var notifications = await this.freelancePlatform
                                            .NotificationsManager
                                                .GetAllUserNotifications<UserNotificationViewModel>(userId);

            this.ViewData["NotificationsCount"] = notifications.Count();

            return this.View(notifications);
        }
    }
}
