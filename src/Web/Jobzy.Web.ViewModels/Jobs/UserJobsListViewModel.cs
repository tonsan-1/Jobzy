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

        public DateTime ExpirationDate => this.DatePosted.AddMonths(1);

        public int OffersCount { get; set; }
    }
}
