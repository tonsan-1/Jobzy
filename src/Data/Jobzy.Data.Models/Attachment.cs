namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Data.Common.Models;

    public class Attachment : BaseDeletableModel<string>
    {
        public Attachment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [FileExtensions]
        public string Extension { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string ContractId { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
