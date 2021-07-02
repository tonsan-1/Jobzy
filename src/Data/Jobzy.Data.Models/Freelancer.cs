namespace Jobzy.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Freelancer : ApplicationUser
    {
        public Freelancer()
        {
            this.Tags = new List<Tag>();
        }

        [Range(3, 150)]
        public int HourlyRate { get; set; }

        public List<Tag> Tags { get; set; }

        // implement more properties such as
        // Jobs Done, Job Success, Recommendation percentage, Skills, Attachments, Work History
    }
}
