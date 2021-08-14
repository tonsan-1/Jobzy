namespace Jobzy.Web.ViewModels.Users.Freelancers
{
    using System.Collections.Generic;

    using Jobzy.Common;

    public class AllFreelancersQueryModel
    {
        public int Rating { get; set; }

        public string Name { get; set; }

        public Sorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public IEnumerable<FreelancerViewModel> Freelancers { get; set; }
    }
}
