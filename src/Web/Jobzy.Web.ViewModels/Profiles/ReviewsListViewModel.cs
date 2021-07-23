namespace Jobzy.Web.ViewModels.Profiles
{
    using System;

    using Jobzy.Data.Models;
    using Jobzy.Services.Mapping;

    public class ReviewsListViewModel : IMapFrom<Review>
    {
        public string Title { get; set; }

        public string ReviewerName { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public DateTime DatePosted { get; set; }

        public string DateFormatted => this.DatePosted.ToLongDateString();
    }
}
