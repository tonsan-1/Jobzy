namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Proposal
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime SentDate => DateTime.UtcNow;

        [Range(1, double.MaxValue)]
        public decimal FixedPrice { get; set; }

        [Range(1, 365)]
        public int DeliveryDays { get; set; }

        public string FreelancerId { get; set; }

        public Freelancer Freelancer { get; set; }

        public string JobId { get; set; }

        public Job Job { get; set; }
    }
}
