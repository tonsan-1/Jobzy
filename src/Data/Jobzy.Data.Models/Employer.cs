namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Employer : ApplicationUser
    {
        public Employer()
        {
            this.EmployerTags = new List<EmployerTag>();
        }

        public List<EmployerTag> EmployerTags { get; set; }

        // implement more properties such as
        // Open Jobs, Reviews..etc
    }
}
