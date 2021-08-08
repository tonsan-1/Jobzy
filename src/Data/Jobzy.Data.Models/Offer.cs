namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        public DateTime AcceptedDate { get; set; }

        public bool IsAccepted { get; set; }

        [Range(1, double.MaxValue)]
        public int FixedPrice { get; set; }

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
