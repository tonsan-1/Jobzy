namespace Jobzy.Web.ViewModels.Messages
{
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class UsersListViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
