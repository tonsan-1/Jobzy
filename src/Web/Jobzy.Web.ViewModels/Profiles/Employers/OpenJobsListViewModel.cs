namespace Jobzy.Web.ViewModels.Employers
{
    using System;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class OpenJobsListViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string JobType { get; set; }

        public string EmployerLocation { get; set; }

        public DateTime DatePosted { get; set; }

        public string DateFormatted => TimeCalculator.GetTimeAgo(this.DatePosted);
    }
}