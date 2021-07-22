namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Review
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DatePosted { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        [Required]
        public string ReviewerId { get; set; }

        public virtual ApplicationUser Reviewer { get; set; }
    }
}
