namespace Jobzy.Web.ViewModels.Profiles
{
    using System.ComponentModel.DataAnnotations;

    public class ProfilePasswordInputModel
    {
        [Required(ErrorMessage = "This field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string RepeatNewPassword { get; set; }
    }
}
