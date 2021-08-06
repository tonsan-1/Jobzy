namespace Jobzy.Web.ViewModels.Messages
{
    using System.ComponentModel.DataAnnotations;

    public class NewMessageInputModel
    {
        [Required]
        public string RecipientUsername { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 1)]
        public string Message { get; set; }
    }
}
