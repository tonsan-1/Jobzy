namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Employer : ApplicationUser
    {
        public List<EmployerTag> EmployerTags => new List<EmployerTag>();

        public List<Contract> Contracts => new List<Contract>();

        // implement more properties such as
        // Open Jobs, Reviews..etc
    }
}
