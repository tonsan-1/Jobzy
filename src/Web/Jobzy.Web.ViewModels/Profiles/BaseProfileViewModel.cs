namespace Jobzy.Web.ViewModels.Profiles
{
    using System.ComponentModel.DataAnnotations;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class BaseProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Name { get; set; }

        public string TagName { get; set; }

        public string Description { get; set; }

        public Country Location { get; set; }

        public string LocationToString => this.Location.GetAttribute<DisplayAttribute>().Name;

        public string ProfileImageUrl { get; set; }


    }
}
