namespace Jobzy.Data.Models
{
    using System;

    public class Proposal
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime SentDate => DateTime.UtcNow;

        public string FreelancerId { get; set; }

        public Freelancer Freelancer { get; set; }

        public string JobId { get; set; }

        public Job Job { get; set; }
    }
}
