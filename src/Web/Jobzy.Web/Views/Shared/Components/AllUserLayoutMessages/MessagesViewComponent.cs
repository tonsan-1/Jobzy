namespace Jobzy.Web.Views.Shared.Components.AllUserMessages
{
    using System.Linq;
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages.AllConversations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "AllUserLayoutMessages")]
    public class MessagesViewComponent : ViewComponent
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public MessagesViewComponent(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUserId = this.userManager.GetUserId(this.UserClaimsPrincipal);
            var userMessages =
                await this.freelancePlatform.MessageManager
                                            .GetAllUserConversations<AllUserConversationsViewModel>(currentUserId);

            foreach (var user in userMessages)
            {
                user.LastMessage =
                    await this.freelancePlatform.MessageManager
                                                .GetConversationLastMessage(currentUserId, user.Id);

                user.ReceivedDate =
                    await this.freelancePlatform.MessageManager
                                                .GetConversationLastMessageSentDate(currentUserId, user.Id);
            }

            this.ViewData["MessagesCount"] = this.freelancePlatform.MessageManager.GetUnreadMessagesCount(currentUserId);

            return this.View(userMessages);
        }
    }
}
