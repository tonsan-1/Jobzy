namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Notification must be between {2} and {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string RedirectUrl { get; set; }

        public bool IsRead { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
