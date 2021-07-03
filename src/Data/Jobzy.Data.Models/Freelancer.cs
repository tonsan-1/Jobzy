namespace Jobzy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Freelancer : ApplicationUser
    {
        public Freelancer()
        {
            this.FreelancerTags = new List<FreelancerTag>();
        }

        [Range(3, 150)]
        public int HourlyRate { get; set; }

        public List<FreelancerTag> FreelancerTags { get; set; }

        // implement more properties such as
        // Jobs Done, Job Success, Recommendation percentage, Skills, Attachments, Work History
    }
}
