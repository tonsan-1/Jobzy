// ReSharper disable VirtualMemberCallInConstructor
namespace Jobzy.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Common.Models;
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string TagName { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }

        public Country Location { get; set; }

        public string ProfileImageUrl { get; set; }

        public bool IsOnline { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<Review> SentReviews { get; set; }
            = new HashSet<Review>();

        public virtual ICollection<Review> ReceivedReviews { get; set; }
            = new HashSet<Review>();

        public virtual ICollection<Notification> Notifications { get; set; }
            = new HashSet<Notification>();

        public virtual ICollection<Message> SentMessages { get; set; }
            = new HashSet<Message>();

        public virtual ICollection<Message> ReceivedMessages { get; set; }
            = new HashSet<Message>();

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
            = new HashSet<IdentityUserRole<string>>();

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
            = new HashSet<IdentityUserClaim<string>>();

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
            = new HashSet<IdentityUserLogin<string>>();
    }
}
