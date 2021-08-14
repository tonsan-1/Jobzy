namespace Jobzy.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;

    public class UserInfoInputModel
    {
        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The {0} must be between {2} and {1} characters long.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Range(1, 40, ErrorMessage = "Choose a nationality.")]
        public Country Location { get; set; }

        public string TagName { get; set; }

        public string Description { get; set; }
    }
}
