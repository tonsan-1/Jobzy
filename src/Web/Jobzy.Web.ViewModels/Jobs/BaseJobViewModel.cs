namespace Jobzy.Web.ViewModels.Jobs
{
    using System;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public abstract class BaseJobViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public JobStatus Status { get; set; }

        public DateTime DatePosted { get; set; }

        public string JobType { get; set; }

        public decimal Budget { get; set; }
    }
}
