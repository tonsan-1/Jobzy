namespace Jobzy.Web.ViewModels.Jobs
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class SingleJobViewModel : IMapFrom<Job>
    {
        public string Title { get; set; }

        public decimal Budget { get; set; }

        public string Description { get; set; }

        public string EmployerName { get; set; }

        public double EmployerRating { get; set; }

        public string EmployerLocation { get; set; }

        public bool EmployerIsVerified { get; set; }
    }
}
