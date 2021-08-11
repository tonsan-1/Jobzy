namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Data.Common.Models;

    public class Notification : BaseDeletableModel<string>
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Notification must be between {2} and {1} characters long.")]
        public string Text { get; set; }

        [Required]
        public string Icon { get; set; }

        [Required]
        public string RedirectAction { get; set; }

        [Required]
        public string RedirectController { get; set; }

        public string RedirectId { get; set; }

        public bool IsRead { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
