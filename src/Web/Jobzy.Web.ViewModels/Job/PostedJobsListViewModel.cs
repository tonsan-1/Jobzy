namespace Jobzy.Web.ViewModels.Job
{
    using System;

    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class PostedJobsListViewModel : IMapFrom<Job>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime DatePosted { get; set; }

        public DateTime ExpirationDate => this.DatePosted.AddMonths(1);

        public int ProposalsCount { get; set; }
    }
}
