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

        [Range(1, 3)]
        public JobStatus Status { get; set; }

        [Required]
        public string CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; }

        public virtual List<Contract> Contracts { get; set; } = new List<Contract>();

        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
