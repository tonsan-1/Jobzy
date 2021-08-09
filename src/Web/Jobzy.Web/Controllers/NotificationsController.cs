namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Employer, Freelancer")]
    public class NotificationsController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationsController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("/MarkNotificationAsRead")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> MarkNotificationAsRead([FromBody]string id)
        {
            var userId = this.userManager.GetUserId(this.User);
            await this.freelancePlatform.NotificationsManager.MarkNotificationAsRead(id);

            var notificationsCount = this.freelancePlatform.NotificationsManager.GetNotificationsCount(userId);

            return this.Json(new { count = notificationsCount });
        }
    }
}
