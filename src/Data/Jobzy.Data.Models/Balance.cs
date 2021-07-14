namespace Jobzy.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Balance
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [DataType(DataType.Currency)]
        public decimal Money { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}
