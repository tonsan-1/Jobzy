namespace Jobzy.Web.Controllers
{
    using System.Threading.Tasks;

    using Jobzy.Data.Models;
    using Jobzy.Services.Interfaces;
    using Jobzy.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = "Freelancer, Employer")]
    public class MessagesController : BaseController
    {
        private readonly IFreelancePlatform freelancePlatform;
        private readonly UserManager<ApplicationUser> userManager;

        public MessagesController(
            IFreelancePlatform freelancePlatform,
            UserManager<ApplicationUser> userManager)
        {
            this.freelancePlatform = freelancePlatform;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Conversation(string id)
        {
            if (id is null)
            {
                return this.View("Error");
            }

            var userId = this.userManager.GetUserId(this.User);

            var conversation = new UserConversationViewModel
            {
                User = await this.freelancePlatform.UserManager.GetUserByIdAsync<UserViewModel>(id),
                Messages = await this.freelancePlatform.MessageManager.GetMessagesAsync<UserMessageViewModel>(userId, id),
            };

            return this.View(conversation);
        }

        public IActionResult All() => this.View();

        [HttpPost]
        public async Task<IActionResult> NewMessage(NewMessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("All");
            }

            var recipient = await this.userManager.FindByNameAsync(input.RecipientUsername);

            if (recipient is null)
            {
                this.ModelState.AddModelError(string.Empty, "Invalid user.");
                return this.View(input);
            }

            var senderId = this.userManager.GetUserId(this.User);
            await this.freelancePlatform.MessageManager.CreateAsync(senderId, recipient.Id, input.Message);

            return this.RedirectToAction("Conversation", new { id = recipient.Id });
        }

        [HttpPost]
        public async Task<IActionResult> MarkAllMessagesAsRead([FromBody] string userId)
        {
            var currentUserId = this.userManager.GetUserId(this.User);
            var messagesCount = this.freelancePlatform.MessageManager.GetUnreadMessagesCount(currentUserId);
            await this.freelancePlatform.MessageManager.MarkAllMessagesAsReadAsync(currentUserId, userId);

            return this.Json(new { count = messagesCount });
        }
    }
}
