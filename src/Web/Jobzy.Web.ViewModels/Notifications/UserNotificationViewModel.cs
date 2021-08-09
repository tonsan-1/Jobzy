namespace Jobzy.Web.ViewModels.Notifications
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserNotificationViewModel : IMapFrom<Notification>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }

        public string RedirectUrl { get; set; }
    }
}
