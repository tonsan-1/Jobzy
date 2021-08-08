namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Common.Models;

    public class Contract : BaseDeletableModel<string>
    {
        public Contract()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public ContractStatus Status { get; set; }

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
