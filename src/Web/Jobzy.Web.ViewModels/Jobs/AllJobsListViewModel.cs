namespace Jobzy.Web.ViewModels.Jobs
{
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class AllJobsListViewModel : BaseJobViewModel, IMapFrom<Job>
    {
        public string Description { get; set; }

        public string EmployerFirstName { get; set; }

        public string EmployerLastName{ get; set; }

        public Country EmployerLocation { get; set; }

        public string EmployerLocationToString => this.EmployerLocation.GetAttribute<DisplayAttribute>().Name;

        public string EmployerProfileImageUrl { get; set; }

        public string TimeAgo => TimeCalculator.GetTimeAgo(this.CreatedOn);
    }
}
