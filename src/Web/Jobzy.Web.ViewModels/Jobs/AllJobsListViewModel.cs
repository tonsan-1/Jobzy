namespace Jobzy.Web.ViewModels.Jobs
{
    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class AllJobsListViewModel : BaseJobViewModel, IMapFrom<Job>
    {
        public string Description { get; set; }

        public string EmployerName { get; set; }

        public string EmployerLocation { get; set; }

        public string EmployerProfileImageUrl { get; set; }

        public string DatePostedFormatted => TimeCalculator.GetTimeAgo(this.CreatedOn);
    }
}
