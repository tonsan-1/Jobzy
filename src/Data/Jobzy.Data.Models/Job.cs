namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class Job
    {
        public Job()
        {
            this.JobTags = new List<JobTag>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public JobTypes JobType { get; set; }

        [Required]
        public JobCategories JobCategory { get; set; }

        [Range(5000, 300000)]
        public decimal MinSalary { get; set; }

        [Range(6000, 1000000)]
        public decimal MaxSalary { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [MaxLength(4096)]
        public string Description { get; set; }

        public DateTime DatePosted { get; set; }

        public string EmployerId { get; set; }

        public Employer Employer { get; set; }

        public List<JobTag> JobTags { get; set; }
    }
}
