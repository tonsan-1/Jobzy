namespace Jobzy.Web.ViewModels.Messages
{
    using System;

    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserMessageViewModel : IMapFrom<Message>
    {
        public string Content { get; set; }

        public string SenderId { get; set; }

        public string SenderProfileImageUrl { get; set; }

        public DateTime DateReceived { get; set; }

        public string DateReceivedToString => this.DateReceived.ToLongDateString();

        public bool IsDateReceivedToday => this.DateReceived.Date == DateTime.Today;
    }
}
