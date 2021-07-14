namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Offer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime SentDate => DateTime.UtcNow;

        [Range(1, double.MaxValue)]
        public decimal FixedPrice { get; set; }

        [Range(1, 365)]
        public int DeliveryDays { get; set; }

        [Required]
        public string FreelancerId { get; set; }

        public virtual Freelancer Freelancer { get; set; }

        [Required]
        public string JobId { get; set; }

        public virtual Job Job { get; set; }
    }
}
