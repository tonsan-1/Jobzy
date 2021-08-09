namespace Jobzy.Data.Models
{
    using System.Collections.Generic;

    public class Freelancer : ApplicationUser
    {
        public virtual List<Contract> Contracts { get; set; } = new List<Contract>();

        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
