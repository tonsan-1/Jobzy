namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Attachment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Required]
        public string ContractId { get; set; }

        public virtual Contract Contract { get; set; }
    }
}
