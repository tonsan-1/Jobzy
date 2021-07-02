namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Employer : ApplicationUser
    {
        public Employer()
        {
            this.Tags = new List<Tag>();
        }

        public List<Tag> Tags { get; set; }

        // implement more properties such as
        // Open Jobs, Reviews..etc
    }
}
