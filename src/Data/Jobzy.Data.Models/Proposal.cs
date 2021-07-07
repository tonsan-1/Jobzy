namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Proposal
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(4096, MinimumLength = 30, ErrorMessage = "{0} length must be in the range 30..4096")]
        [Display(Name = "Proposal")]
        public string Description { get; set; }

        public DateTime SentDate { get; set; }

        public Freelancer Freelancer { get; set; }

        public Job Job { get; set; }
    }
}
