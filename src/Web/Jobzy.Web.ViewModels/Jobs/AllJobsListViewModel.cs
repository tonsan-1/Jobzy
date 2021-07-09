namespace Jobzy.Web.ViewModels.Jobs
{
    using System;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class AllJobsListViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string JobType { get; set; }

        public DateTime DatePosted { get; set; }

        public string Description { get; set; }

        public decimal Budget { get; set; }

        public string EmployerName { get; set; }

        public string EmployerLocation { get; set; }

        public string EmployerProfileImageUrl { get; set; }

        public string DatePostedFormatted => TimeCalculator.GetTimeAgo(this.DatePosted);
    }
}
