namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Job
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string EmployerId { get; set; }

        public virtual Employer Employer { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public JobType JobType { get; set; }

        [Required]
        public JobCategory JobCategory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public bool IsClosed { get; set; } = false;

        public bool IsDeleted { get; set; } = false;

        public virtual List<Offer> Offers { get; set; } = new List<Offer>();

        public virtual List<JobTag> JobTags { get; set; } = new List<JobTag>();
    }
}
