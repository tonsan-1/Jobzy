namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Employer : ApplicationUser
    {
        public virtual List<Contract> Contracts { get; set; } = new List<Contract>();

        public virtual List<Job> Jobs { get; set; } = new List<Job>();

        // implement more properties such as
        // Open Jobs, Reviews..etc
    }
}
