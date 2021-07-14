namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Freelancer : ApplicationUser
    {
        public List<Contract> Contracts => new List<Contract>();

        public List<FreelancerTag> FreelancerTags => new List<FreelancerTag>();

        // implement more properties such as
        // Jobs Done, Job Success, Recommendation percentage, Skills, Attachments, Work History
    }
}
