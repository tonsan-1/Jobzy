namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Data.Common.Models;

    public class Review : BaseDeletableModel<string>
    {

        public Review()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, 5)]
        public double Rating { get; set; }

        public string ReviewerName { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
