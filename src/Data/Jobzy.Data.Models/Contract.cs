namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Contract
    {
        public Contract()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public ContractStatus Status { get; set; }

        [Required]
        public string FreelancerId { get; set; }

        public Freelancer Freelancer { get; set; }

        [Required]
        public string EmployerId { get; set; }

        public Employer Employer { get; set; }

        [Required]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }
    }
}
