namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Job
    {
        public int Id { get; set; }

        public Employer Employer { get; set; }

        public Freelancer Freelancer { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public JobTypes JobType { get; set; }

        [Required]
        public JobCategories JobCategory { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public bool IsClosed { get; set; } = false;

        public bool IsPaymentDenied { get; set; } = false;

        public List<Proposal> Proposals { get; set; } = new List<Proposal>();

        public List<JobTag> JobTags { get; set; } = new List<JobTag>();
    }
}
