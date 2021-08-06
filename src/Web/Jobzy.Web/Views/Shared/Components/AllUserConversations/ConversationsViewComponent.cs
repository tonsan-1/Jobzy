namespace Jobzy.Web.ViewComponents
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages.AllConversations;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [ViewComponent(Name = "AllUserConversations")]
    public class ConversationsViewComponent : ViewComponent
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public ConversationsViewComponent(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentUserId = this.userManager.GetUserId(this.UserClaimsPrincipal);
            var userConversations =
                await this.freelancePlatform.MessageManager
                                            .GetAllUserConversations<AllUserConversationsViewModel>(currentUserId);

            foreach (var user in userConversations)
            {
                user.LastMessage =
                    await this.freelancePlatform.MessageManager
                                                .GetConversationLastMessage(currentUserId, user.Id);
                user.ReceivedDate =
                    await this.freelancePlatform.MessageManager
                                                .GetConversationLastMessageSentDate(currentUserId, user.Id);
            }

            return this.View(userConversations);
        }
    }
}
