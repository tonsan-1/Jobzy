namespace Jobzy.Web.Hubs
{
    using System;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class OnlineUsersHub : Hub
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public OnlineUsersHub(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();

            var userId = this.userManager.GetUserId(this.Context.User);

            await this.freelancePlatform.UserManager
                .UpdateUserOnlineStatusAsync("online", userId);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);

            var userId = this.userManager.GetUserId(this.Context.User);

            await this.freelancePlatform.UserManager
                .UpdateUserOnlineStatusAsync("offline", userId);
        }
    }
}
