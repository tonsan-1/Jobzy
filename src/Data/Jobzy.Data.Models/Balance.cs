namespace Jobzy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Balance
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Money { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}
