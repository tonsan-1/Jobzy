namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Employer : ApplicationUser
    {
        public virtual List<EmployerTag> EmployerTags { get; set; } = new List<EmployerTag>();

        public virtual List<Contract> EmployerContracts { get; set; } = new List<Contract>();

        // implement more properties such as
        // Open Jobs, Reviews..etc
    }
}
