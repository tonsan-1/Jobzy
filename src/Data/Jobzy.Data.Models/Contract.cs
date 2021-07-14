namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Contract
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ContractStatus Status { get; set; }

        [Required]
        public string FreelancerId { get; set; }

        public virtual Freelancer Freelancer { get; set; }

        [Required]
        public string EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        [Required]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }
    }
}
