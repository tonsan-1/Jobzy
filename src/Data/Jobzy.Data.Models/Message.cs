namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        public DateTime DateReceived { get; set; } = DateTime.UtcNow;

        [Required]
        public string RecipientId { get; set; }

        public virtual ApplicationUser Recipient { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public bool IsRead { get; set; }
    }
}
