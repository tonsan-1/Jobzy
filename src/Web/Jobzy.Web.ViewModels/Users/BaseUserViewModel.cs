namespace Jobzy.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using Jobzy.Common;
    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;
    using Jobzy.Web.ViewModels.Reviews;

    public class BaseUserViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string TagName { get; set; }

        public string Description { get; set; }

        public string Email { get; set; }

        public double AverageRating
            => this.Reviews.Any() ? Math.Round(this.Reviews.Select(x => x.Rating).Average(), 2) : 0;

        public Country Location { get; set; }

        public string LocationToString => this.Location.GetAttribute<DisplayAttribute>().Name;

        public string ProfileImageUrl { get; set; }

        public IEnumerable<ReviewsListViewModel> Reviews { get; set; } = new HashSet<ReviewsListViewModel>();
    }
}
