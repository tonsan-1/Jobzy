namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Content { get; set; }

        public DateTime DateReceived { get; set; }

        [Required]
        public string RecipientId { get; set; }

        [Required]
        public string SenderId { get; set; }

        [NotMapped]
        public bool IsMine { get; set; }

        [NotMapped]
        public virtual ApplicationUser Sender { get; set; }

        [NotMapped]
        public virtual ApplicationUser Recipient { get; set; }
    }
}
