namespace Jobzy.Web.ViewModels.Messages
{
    using System.Collections.Generic;

    public class UserConversationViewModel
    {
        public UserViewModel User { get; set; }

        public IEnumerable<UserMessageViewModel> Messages { get; set; }
    }
}
