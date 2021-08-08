namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Data.Common.Models;

    public class Message : BaseDeletableModel<string>
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual ApplicationUser Recipient { get; set; }

        [Required]
        public string SenderId { get; set; }

        public virtual ApplicationUser Sender { get; set; }

        public bool IsRead { get; set; }
    }
}
