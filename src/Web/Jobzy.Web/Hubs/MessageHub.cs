namespace Jobzy.Web.Hubs
{
    using System;
    using System.Threading.Tasks;
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;

    public class MessageHub : Hub
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public MessageHub(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task SendMessageToUser(string receiverId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var currentUser = await this.userManager.GetUserAsync(this.Context.User);

            await this.freelancePlatform.MessageManager.CreateAsync(currentUser.Id, receiverId, message);

            await this.Clients
                .Users(new string[] { currentUser.Id, receiverId })
                .SendAsync("ReceiveMessage", new
            {
                Content = message,
                SenderId = currentUser.Id,
                ReceiverId = receiverId,
                SenderProfileImageUrl = currentUser.ProfileImageUrl,
            });
        }
    }
}
