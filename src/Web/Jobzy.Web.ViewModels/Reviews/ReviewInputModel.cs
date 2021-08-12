namespace Jobzy.Web.ViewModels.Reviews
{
    using System.ComponentModel.DataAnnotations;

    public class ReviewInputModel
    {
        [Required]
        public string SenderId { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public string RecipientFirstName { get; set; }

        public string RecipientLastName { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
