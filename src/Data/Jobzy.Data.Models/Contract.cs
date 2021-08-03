namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Contract
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ContractStatus Status { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime CompletedOn { get; set; }

        [Required]
        public string FreelancerId { get; set; }

        public virtual Freelancer Freelancer { get; set; }

        [Required]
        public string EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        [Required]
        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        public string JobId { get; set; }

        public virtual Job Job { get; set; }

        public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();
    }
}
