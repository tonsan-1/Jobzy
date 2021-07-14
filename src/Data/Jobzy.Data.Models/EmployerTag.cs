namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EmployerTag
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(25)]
        public string Text { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
