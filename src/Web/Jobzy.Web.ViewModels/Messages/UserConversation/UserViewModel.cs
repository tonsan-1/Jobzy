namespace Jobzy.Web.ViewModels.Messages
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string ProfileImageUrl { get; set; }
    }
}
