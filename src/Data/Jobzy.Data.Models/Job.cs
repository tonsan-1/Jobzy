namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Common.Models;

    public class Job : BaseDeletableModel<string>
    {
        public Job()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

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
        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual List<Contract> Contracts { get; set; } = new List<Contract>();

        public virtual List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
