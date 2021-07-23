namespace Jobzy.Web.ViewModels.Profiles
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class BaseProfileViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string TagName { get; set; }

        public string Description { get; set; }

        public double AverageRating
            => this.Reviews.Any() ? this.Reviews.Select(x => x.Rating).Average() : 0;

        public Country Location { get; set; }

        public string LocationToString => this.Location.GetAttribute<DisplayAttribute>().Name;

        public string ProfileImageUrl { get; set; }

        public List<ReviewsListViewModel> Reviews { get; set; } = new List<ReviewsListViewModel>();
    }
}
