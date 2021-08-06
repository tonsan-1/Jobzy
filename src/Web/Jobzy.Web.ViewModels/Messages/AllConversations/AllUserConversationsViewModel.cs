namespace Jobzy.Web.ViewModels.Messages.AllConversations
{
    using System;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class AllUserConversationsViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfileImageUrl { get; set; }

        public string LastMessage { get; set; }

        public DateTime ReceivedDate { get; set; }

        public string ReceivedDateTimeAgo => TimeCalculator.GetTimeAgo(this.ReceivedDate);
    }
}
