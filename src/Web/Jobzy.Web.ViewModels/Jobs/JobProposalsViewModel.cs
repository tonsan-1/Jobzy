namespace Jobzy.Web.ViewModels.Jobs
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class JobProposalsViewModel : IMapFrom<Proposal>
    {
        public string JobId { get; set; }

        public string JobTitle { get; set; }

        public string FreelancerId { get; set; }

        public string FreelancerName { get; set; }

        public string FreelancerEmail { get; set; }

        public string FreelancerRating { get; set; }

        public string FreelancerProfileImageUrl { get; set; }
    }
}
