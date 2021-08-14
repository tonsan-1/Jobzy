namespace Jobzy.Web.ViewModels.Users
{
    public class UserSettingsViewModel
    {
        public BaseUserViewModel ProfileViewModel { get; set; }

        public UserInfoInputModel ProfileInfoInputModel { get; set; }

        public UserPasswordInputModel ProfilePasswordInputModel { get; set; }
    }
}
