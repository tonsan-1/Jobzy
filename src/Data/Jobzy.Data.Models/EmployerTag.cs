namespace Jobzy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class EmployerTag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Text { get; set; }
    }
}
