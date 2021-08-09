namespace Jobzy.Web.ViewModels.Notifications
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class JobNotificationViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string EmployerId { get; set; }
    }
}
