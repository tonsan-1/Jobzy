namespace Jobzy.Web.ViewModels.Jobs
{
    using System;

    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserJobsListViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime DatePosted { get; set; }

        public int OffersCount { get; set; }

        public bool IsClosed { get; set; }

        public string Status => this.IsClosed ? "In Active Contract" : "Open";

        public string ButtonColor => this.IsClosed ? "yellow" : "green";
    }
}
